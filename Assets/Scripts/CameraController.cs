using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform lookAt;
    private Vector3 offset;

    void Start()
    {
        offset = lookAt.position - transform.position;
    }

    void Update()
    {
        transform.position = lookAt.position - offset;
    }
}
