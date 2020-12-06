using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take : MonoBehaviour
{
    Rigidbody attachedBody;
    enum TakeState { attaching, attached }
    TakeState currentTakeState;

    [SerializeField] Transform attachTransform;
    Vector3 initialPosition;
    Quaternion initialRotation;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float throwForce = 200f;

    public Transform potentialBodyToAttach;


    private void Update()
    {
        if (!attachedBody && Input.GetButtonDown("Grab"))
        {
            attachedBody = tryToTake();
        }
        else if (attachedBody && (Input.GetButtonDown("Grab")))
        {
            AudioManager.PlaySound("kick");
            detachObject(throwForce);
        }
        else
        {
            if (attachedBody != null)
            {
                switch (currentTakeState)
                {
                    case TakeState.attaching:
                        updateAttaching();
                        break;
                    case TakeState.attached:
                        updateAttached();
                        break;
                }
            }
        }


    }

    private void updateAttaching()
    {
        attachedBody.position = attachTransform.position;
        if ((attachedBody.position - attachTransform.position).magnitude < 0.1f)
            currentTakeState = TakeState.attached;
    }

    private void updateAttached()
    {
        attachedBody.transform.position = attachTransform.position;
        attachedBody.transform.rotation = attachTransform.rotation;
    }

    private void detachObject(float force)
    {
        attachedBody.isKinematic = false;
        attachedBody.AddForce(attachTransform.forward * force);
        attachedBody.GetComponent<BoxCollider>().enabled = true;
        attachedBody = null;
    }

    private Rigidbody tryToTake()
    {
        if (potentialBodyToAttach != null)
        {
            Rigidbody rb = potentialBodyToAttach.GetComponent<Rigidbody>();
            if (rb == null) return null;
            rb.isKinematic = true;
            initialPosition = rb.transform.position;
            initialRotation = rb.transform.rotation;
            currentTakeState = TakeState.attaching;
            potentialBodyToAttach.GetComponent<BoxCollider>().enabled = false;
            AudioManager.PlaySound("grab");
            return rb;
        }
        
        return null;
    }
}
