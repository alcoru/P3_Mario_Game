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
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("holaaaaa");
        
        bridgeRigidBody.AddForceAtPosition(-hit.normal*bridgeForce, hit.point);
    }
}
