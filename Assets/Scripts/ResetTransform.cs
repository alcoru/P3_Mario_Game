using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour, IRestartGame
{
   [SerializeField] Transform initialTransform;

    Vector3 initPos;
    Quaternion initRot;
    public void Restart()
    {
        gameObject.SetActive(false);
        if (initialTransform != null)
        {
            transform.position = initialTransform.position;
            transform.rotation = initialTransform.rotation;
        }
        else
        {
            transform.position = initPos;
            transform.rotation = initRot;
        }
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    /*void Start()
    {
        //GameManager.getInstance().addRestartGameObject(this);
        GameManager.RestartGameEvent += Restart;
        initPos = transform.position;
        initRot = transform.rotation;

    }*/


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
}
