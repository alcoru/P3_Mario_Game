using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    private int lifes = 3;

    public void LooseLife()
    {
        lifes --;
        if(lifes < 0)
            Debug.Log("GameOver no Retries");
    }

    public void GainLife()
    {
        lifes ++;
    }

    public int LeftLifes()
    {
        return lifes;
    }
}
