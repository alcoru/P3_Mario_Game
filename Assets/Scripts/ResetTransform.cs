using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour, IRestartGame
{
   Transform currentPositonTransform;

    Vector3 initPos;
    Quaternion initRot;
    public void Restart()
    {
        gameObject.SetActive(false);
        if (currentPositonTransform != null)
        {
            transform.position = currentPositonTransform.position;
            transform.rotation = currentPositonTransform.rotation;
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
        currentPositonTransform = transform;
    }
}
