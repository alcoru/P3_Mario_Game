using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    Animator animator;
    private int maxHealth = 8;
    private int currentHealth;

    [SerializeField] HealthBar healthBar;

    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    public void DealDamage(int damage = 1)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
           Debug.Log("Game Over"); 
           animator.SetBool("Dead", true);
        }
        healthBar.UpdateHealthBar(currentHealth);
        animator.SetTrigger("Hit");
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
