using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] int health = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().AddHealth(health);
            AudioManager.PlaySound("upgrade");
            Destroy(gameObject);
        }
            
    }
}