using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMarioAttack : MonoBehaviour
{
    public enum PunchType { RIGHT, LEFT }
    public void updatePunch (PunchType punch, bool active)
    {

    }
    [SerializeField] GameObject DamagePoint;
    private int punch = 0;
    private Animator animator;
    [SerializeField] float initialTimeToPunch;
    private float timeToPunch;
    private bool isPunchFinished = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
        DamagePoint.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isPunchFinished)
        {
            punch ++;
            if(punch > 3) punch = 1;
            
            switch(punch)
            {
                case 1:
                    animator.SetTrigger("Punch1");
                    AudioManager.PlaySound("punch1");
                    break;
                case 2:
                    animator.SetTrigger("Punch2");
                    AudioManager.PlaySound("punch2");
                    break;
                default:
                    animator.SetTrigger("Punch3");
                    AudioManager.PlaySound("kick");
                    break;
            }
        }
        else
        {
            if(isPunchFinished && timeToPunch < 0)
                punch = 0;
        }
        
        timeToPunch -= Time.deltaTime;
    }

    public void StartPunch()
    {
        isPunchFinished = false;
        DamagePoint.GetComponent<BoxCollider>().enabled = true;
    }

    public void EndPunch()
    {
        timeToPunch = initialTimeToPunch;
        isPunchFinished = true;
        DamagePoint.GetComponent<BoxCollider>().enabled = false;
    }

}
