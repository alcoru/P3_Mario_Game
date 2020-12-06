using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject mario;
    private LifeSystem lifeSystem;

    private bool gameOver = false;
    
    void Awake()
    {
        lifeSystem = mario.GetComponent<LifeSystem>();
    }
    
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.0f;
        gameOver = true;
        Cursor.lockState = CursorLockMode.None;
        
        lifeSystem.LooseLife();

        if(lifeSystem.LeftLifes() <= 0)
            retryButton.SetActive(false);
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    void Restart()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1.0f;
        gameOver = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected void OnEnable()
    {        
        GameManager.RestartGameEvent += Restart;
    }

    private void OnDisable()
    {        
        GameManager.RestartGameEvent -= Restart;       
    }

}
