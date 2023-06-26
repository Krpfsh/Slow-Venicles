using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarLights : MonoBehaviour
{
    public enum Side
    {
        Front,
        Back
    }
    [System.Serializable]
    public struct Light
    {
        public GameObject lightObj;
        public Side side;
    }

    //lights
    public Toggle lightToggle;
    public bool isFrontLightOn;
    public bool isBackLightOn;
    public List<Light> lights;

    private void Start()
    {
        isFrontLightOn = lightToggle.isOn;
        isBackLightOn = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PauseMenu.GameIsPaused == false)
        {
            OperateFrontLights();
        }
    }
    public void OperateFrontLights()
    {
        isFrontLightOn = !isFrontLightOn;

        if (isFrontLightOn)
        {
            //turn on lights
            foreach (var light in lights)
            {
                if (light.side == Side.Front && light.lightObj.activeInHierarchy == false)
                {
                    light.lightObj.SetActive(true);
                }
            }
        }
        else
        {
            //turn off lights
            foreach (var light in lights)
            {
                if (light.side == Side.Front && light.lightObj.activeInHierarchy == true)
                {
                    light.lightObj.SetActive(false);
                }
            }
        }
    }
    public void OperateBackLights()
    {
        if (isBackLightOn)
        {
            //turn on lights
            foreach (var light in lights)
            {
                if (light.side == Side.Back && light.lightObj.activeInHierarchy == false)
                {
                    light.lightObj.SetActive(true);
                }
            }
        }
        else
        {
            foreach (var light in lights)
            {
                if (light.side == Side.Back && light.lightObj.activeInHierarchy == true)
                {
                    light.lightObj.SetActive(false);
                }

            }
        }
    }
}

