using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] BoxCollider boxColliderA;
    [SerializeField] BoxCollider boxColliderB;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() != null)
        {
            other.gameObject.transform.SetParent(transform);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() != null)
        {
            other.gameObject.transform.SetParent(null);
        }
    }


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
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<CharacterController>() != null)
            {
                child.gameObject.transform.SetParent(null);
            }
        }


    }
}
