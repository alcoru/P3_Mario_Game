using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour, IRestartGame
{

   Transform currentPositionTransform;

    Vector3 initPos;
    Quaternion initRot;
    public void Restart()
    {
        gameObject.SetActive(false);
        if (currentPositionTransform != null)
        {
            transform.position = currentPositionTransform.position;
            transform.rotation = currentPositionTransform.rotation;

        }
        else
        {
            transform.position = initPos;
            transform.rotation = initRot;
        }
        gameObject.SetActive(true);
    }


    private void OnDisable()
    {        
        GameManager.RestartGameEvent -= Restart;       
    }

    protected void OnEnable()
    {        
        GameManager.RestartGameEvent += Restart;
        initPos = transform.position;
        initRot = transform.rotation;
    }

    public void SetCurrentPositionTransform(Transform transform)
    {
        currentPositionTransform = transform;
    }
}
