using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject timeLapsUI;
    public GameObject car;
    // Update is called once per frame
    private AudioSource _source;
    private void Start()
    {
         _source = car.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                //Resume();
            }else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        _source.Play();
        //AudioListener.volume = 1;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        timeLapsUI.SetActive(true);
    }
    void Pause()
    {
        _source.Stop();
        //AudioListener.volume = 0;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused= true;
        timeLapsUI.SetActive(false);
    }
}
