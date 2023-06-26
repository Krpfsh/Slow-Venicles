using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum ControlType { HumanInput, AI }
    public ControlType controlType  =  ControlType.HumanInput;

    public float BestLapTime { get; private set; } = Mathf.Infinity;

    public float LastLapTime { get; private set; } = 0;
    public float CurrentLapTime { get; private set;} = 0;
    public int CurrentLap { get; private set; } = 0;

    private int lastCheckpointPassed = 0;
    private float lapTimerTimestamp;
    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    private static CarControllerOld carController;

    private void Awake()
    {
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount =checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        carController = GetComponent<CarControllerOld>();

    }
    void StartLap()
    {
        Debug.Log("start");
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimestamp = Time.time; //7.75
    }
    void EndLap()
    {
        LastLapTime= Time.time - lapTimerTimestamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
        Debug.Log($"end  - {LastLapTime}");
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer != checkpointLayer)
        {
            return;

        }
        if (collider.gameObject.name == "StartPoint")
        {
            if (lastCheckpointPassed == checkpointCount)
            {
                EndLap();
            }
            if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }
            return;
        }
        if (collider.gameObject.name == (lastCheckpointPassed + 1).ToString())
        {
            lastCheckpointPassed++;
        }
    }
    
    void Update()
    {
        CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0; //10.75 - 7.75 = 3;
        if(controlType == ControlType.HumanInput)
        {
            CarControllerOld.Steer = GameManager.Instance.InputController.SteerInput;
            CarControllerOld.Throttle = GameManager.Instance.InputController.ThrottleInput;
        }
    }
}