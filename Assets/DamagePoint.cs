using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePoint : MonoBehaviour
{
    void OnTriggerEnter(Collider hit)
    {
        if(hit.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.KillEnemy();
    }

    void OnTriggerStay(Collider hit)
    {
        if(hit.GetComponent<Enemy>())
            hit.GetComponent<Enemy>().KillEnemy();
    }
}
