using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBehaviour : StateMachineBehaviour
{
    
    [SerializeField] SuperMarioAttack.PunchType punchType;
    [SerializeField] [Range(0.0f, 1.0f)] float punchStartTime;
    [SerializeField] [Range(0.0f, 1.0f)] float punchEndTime;

    SuperMarioAttack attack;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attack = animator.GetComponent<SuperMarioAttack>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isActive = stateInfo.normalizedTime > punchStartTime && stateInfo.normalizedTime < punchEndTime;
        attack.updatePunch(punchType, isActive);
    }
}
