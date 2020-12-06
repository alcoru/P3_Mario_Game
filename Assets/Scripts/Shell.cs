using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Take>().potentialBodyToAttach = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Take>().potentialBodyToAttach = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(rb.velocity.magnitude > 1f && !rb.isKinematic)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.transform.GetComponent<HealthSystem>().DealDamage();
            }
            else if (collision.collider.CompareTag("Enemy"))
            {
                collision.transform.GetComponent<Enemy>().KillEnemy();
            }
                
        }
        
    }
}
