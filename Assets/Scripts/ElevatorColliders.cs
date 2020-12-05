using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorColliders : MonoBehaviour
{
    [SerializeField] BoxCollider boxColliderA;
    [SerializeField] BoxCollider boxColliderB;
    
    void activateColliderA()
    {
        boxColliderA.enabled = true;
        boxColliderB.enabled = false;
    }

    void activateColliderB()
    {
        boxColliderA.enabled = false;
        boxColliderB.enabled = true;
    }

    void deactivateColliders()
    {
        boxColliderA.enabled = false;
        boxColliderB.enabled = false;
    }
}
