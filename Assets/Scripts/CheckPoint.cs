using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<ResetTransform>(out ResetTransform resetTransform))
        {
            resetTransform.SetCurrentPositionTransform(transform);
        }
    }

}

