using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject UIRacePanel;

    public TMP_Text UITextCurrentLap;
    public TMP_Text UITextCurrentTime;
    public TMP_Text UITextLastLapTime;

    public TimeLaps UpdateUIForPlayer;

    private int currentLap = -1;
    private float currentLapTime;
    private float lastLapTime;

    void Update()
    {
        if (UpdateUIForPlayer == null)
        {
            return;
        }
        if (UpdateUIForPlayer.CurrentLap != currentLap)
        {
            currentLap = UpdateUIForPlayer.CurrentLap;
            UITextCurrentLap.text = $"Круг: {currentLap}";
        }
        if (UpdateUIForPlayer.CurrentLapTime != currentLapTime)
        {
            currentLapTime = UpdateUIForPlayer.CurrentLapTime;
            UITextCurrentTime.text = $"Время: {(int)currentLapTime / 60}:{(currentLapTime) % 60:00.000}".Replace(',','.');
        }
        if (UpdateUIForPlayer.LastLapTime != lastLapTime)
        {
            lastLapTime = UpdateUIForPlayer.LastLapTime;
            UITextLastLapTime.text = $"Последнее время круга: {(int)lastLapTime / 60}:{(lastLapTime) % 60:00.000}".Replace(',', '.');
        }
    }
}
