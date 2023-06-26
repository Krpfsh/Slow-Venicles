using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using static CarController;
using UnityEngine.SceneManagement;


public class CarController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }
    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public ParticleSystem smokeParticle;
        public GameObject wheelEffectObject;
        public Axel axel;
    }
    public float maxAxeleration = 30f;
    public float brakeAcceleration = 50f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;
    public List<Wheel> wheels;

    float moveInput;
    float steerInput;
    private Rigidbody carRb;
    public GameObject carGO;

    //lights
    private CarLights carLights;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
        GameObject carGO = GetComponent<GameObject>();
        carLights = GetComponent<CarLights>();
    }

    private void Update()
    {
        GetInputs();
        AnimateWheels();
        WheelEffects();

        if (Input.GetKeyDown(KeyCode.R) && PauseMenu.GameIsPaused == false)
        {
            SceneManager.LoadScene("Game");
        }
    }
    private void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }
    private void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }
    private void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * maxAxeleration;
        }
    }
    private void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }
    private void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;

            }

            carLights.isBackLightOn = true;
            carLights.OperateBackLights();
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
            carLights.isBackLightOn = false;
            carLights.OperateBackLights();
        }
    }
    private void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
    //SkidMark
    private void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.velocity.magnitude >= 10.0f)
            {
                wheel.wheelEffectObject.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else
            {
                wheel.wheelEffectObject.GetComponentInChildren<TrailRenderer>().emitting = false;
            }


        }
    }
}

