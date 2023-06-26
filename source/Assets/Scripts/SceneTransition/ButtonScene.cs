using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    public void GoToGame()
    {
        SceneTransition.SwitchToScene("Game");
    }
}
