using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMotor : MonoBehaviour
{
    Animator uiAnim;
    // Start is called before the first frame update
    void Start()
    {
        uiAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            uiAnim.SetBool("show", true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            uiAnim.SetBool("show", false);
        }
    }
}
