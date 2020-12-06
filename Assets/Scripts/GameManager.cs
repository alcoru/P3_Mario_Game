using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
public delegate void RestartGameDelegate();

    public static event RestartGameDelegate RestartGameEvent;

    public void RestartGame()
    {
        RestartGameEvent.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void Retry()
    {
        RestartGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
