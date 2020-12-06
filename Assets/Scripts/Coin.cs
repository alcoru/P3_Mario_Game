using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] Score score;
    private Text textScore;

    void Awake()
    {
        textScore = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
    }
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
