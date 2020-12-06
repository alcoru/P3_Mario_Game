using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    private GameOverSystem gameOverSystem;
    Animator animator;
    private int maxHealth = 8;
    private int currentHealth;

    [SerializeField] HealthBar healthBar;

    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        gameOverSystem = GetComponent<GameOverSystem>();
    }
    public void DealDamage(int damage = 1)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
           animator.SetBool("Dead", true);
           //WaitTillAnimationIsFinished();
           gameManager.GetComponent<GameOverSystem>().GameOver();
        }
        healthBar.UpdateHealthBar(currentHealth);
        animator.SetTrigger("Hit");
        AudioManager.PlaySound("take_damage");
    }
    
    void Restart()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth);
    }

    IEnumerator WaitTillAnimationIsFinished()
    {
        yield return new WaitForSeconds(1.0f);
    }

    protected void OnEnable()
    {        
        GameManager.RestartGameEvent += Restart;
    }

    private void OnDisable()
    {        
        GameManager.RestartGameEvent -= Restart;       
    }
    
    public void AddHealth(int health = 1)
    {
        currentHealth += health;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.UpdateHealthBar(currentHealth);
    }
}
