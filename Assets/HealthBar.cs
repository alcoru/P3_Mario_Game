using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Animator animator;
    [SerializeField] Image image;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateHealthBar(int health)
    {
        animator.SetTrigger("Update");
        image.fillAmount = 0.125f * health;

    }
}
