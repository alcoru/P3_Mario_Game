using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] float horizontalSpeed = 10f;
    [SerializeField] float verticalSpeed = 150f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] LayerMask layerMask;

    [SerializeField] float wallThreshold=0.1f;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isWalled() && !playerController.isGrounded())
        {
            StartCoroutine(Jump());
        }
        
    }

    private bool isWalled()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.Raycast(ray, out RaycastHit hit, wallThreshold, layerMask);
    }

    IEnumerator Jump()
    {
        float time = dashTime;
        playerController.doWallJump(verticalSpeed);
        playerController.activateMove(false);
        transform.forward = -transform.forward;
        while (time > 0)
        {
            time -= Time.deltaTime;
            playerController.characterController.Move(transform.forward * horizontalSpeed * Time.deltaTime);

            yield return null;
        }
        playerController.activateMove(true);
    }
}
