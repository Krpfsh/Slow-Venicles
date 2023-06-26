using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeLaps : MonoBehaviour
{
    public static float BestLapTime;
    public TMP_Text bestText;
    public float LastLapTime { get; private set; } = 0;
    public float CurrentLapTime { get; private set; } = 0;
    public int CurrentLap { get; private set; } = 0;

    private int lastCheckpointPassed = 0;
    private float lapTimerTimestamp;
    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;

    private void Awake()
    {
        BestLapTime = PlayerPrefs.GetFloat("BestLap");
        if (BestLapTime == 0)
        {
            BestLapTime = Mathf.Infinity;
        }
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    }
    void StartLap()
    {
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimestamp = Time.time; //7.75
    }
    void EndLap()
    {
        LastLapTime = Time.time - lapTimerTimestamp;

        Debug.Log(BestLapTime);
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
        Debug.Log(BestLapTime);

        PlayerPrefs.SetFloat("BestLap", BestLapTime);
        Debug.Log("End");
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
    // Update is called once per frame
    void Update()
    {
        bestText.text = BestLapTime < 1000000 ? $"Лучшее время круга: {(int)BestLapTime / 60}:{(BestLapTime) % 60:00.000}".Replace(',', '.') : "Лучшее время круга: 00:00:00";
        
        CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0; //10.75 - 7.75 = 3;
    }
}
