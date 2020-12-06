using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Score score;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            if (score != null)
                score.score();
            AudioManager.PlaySound("coin");
            Destroy(gameObject);
        }
        
    } 
}
