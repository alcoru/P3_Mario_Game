using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
public delegate void RestartGameDelegate();

    public static event RestartGameDelegate RestartGameEvent;

    public void RestartGame()
    {
        /*foreach(IRestartGame restart in restartGameObjects)
        {
            restart.Restart();
        }*/
        RestartGameEvent.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}
