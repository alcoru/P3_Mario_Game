using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HEEEEY");
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameOverSystem>().GameOver();
        }
    }
}
