using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongJump : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] float horizontalSpeed = 10f;
    [SerializeField] float verticalSpeed = 225f;
    [SerializeField] float dashTime = 1f;
    bool canLongJump = true;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LongJump") && canLongJump)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canLongJump = false;
        float time = dashTime;
        playerController.doLongJump(verticalSpeed);
        while(time>0)
        {
            time -= Time.deltaTime;
            playerController.characterController.Move(transform.forward * horizontalSpeed * Time.deltaTime);

            yield return null;
        }
        canLongJump = true;
    }
}
