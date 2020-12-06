using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePlayerCollision : MonoBehaviour
{
    Rigidbody bridgeRigidBody;
    [SerializeField] float bridgeForce;

    public void Start()
    {
        bridgeRigidBody = GetComponent<Rigidbody>();
    }
    public void AddForce(Vector3 normal, Vector3 point)
    {   
        bridgeRigidBody.AddForceAtPosition(-normal * bridgeForce, point);
    }
}
