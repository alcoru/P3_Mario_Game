using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] Text lifestext;
    private int lifes = 3;

    void Awake()
    {
        UpdateLifesText();
    }

    public void LooseLife()
    {
        lifes --;
        UpdateLifesText();
    }

    public void GainLife()
    {
        lifes ++;
        UpdateLifesText();
    }

    public int LeftLifes()
    {
        return lifes;
    }

    void UpdateLifesText()
    {
        lifestext.text = lifes.ToString("0");
    }
}
