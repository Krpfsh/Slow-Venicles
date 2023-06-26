using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour
{
    public string loadLevel;
    public GameObject loadingScreen;
    public Text LoadingPercentage;
    public Image LoadingProgressBar;
    
    public void Load()
    {
        loadingScreen.SetActive(true);
        //SceneManager.LoadScene(loadLevel);
        StartCoroutine(LoadAsync());
    }
    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadLevel);

        asyncLoad.allowSceneActivation = false;
        while(!asyncLoad.isDone)
        {
            

            LoadingPercentage.text = Mathf.RoundToInt(asyncLoad.progress * 100+10) + "%";
            LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, asyncLoad.progress+0.1f,Time.deltaTime * 5);
            if (asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
            {
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            
            
            yield return null;
        }
    }
}
