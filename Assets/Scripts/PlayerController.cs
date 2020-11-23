using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    Transform initialPosition;

    CharacterController characterController;

    [SerializeField] Transform camera;

    [SerializeField] float speed = 2.0f;
    [SerializeField] float speedMultiplier = 2.0f;

    public bool mOnGround;
    [SerializeField] float mHeightJump;
    [SerializeField] float mHalfLengthJump;
    [SerializeField] float mDownGravityMultiplier;
    [SerializeField] float mJumpMultiplier;
    private int extraJumps;
    private int maxExtraJumps = 0;

    float mVerticalSpeed = 0.0f;

    private bool mDoJump = false;

    [SerializeField] LayerMask layerMask;

    [SerializeField] float groundThreshold;


    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        initialPosition = transform;
    }

    void Start()
    {
    }

    void Update()
    {
        if(CanJump()) mDoJump = true;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 l_Movement = new Vector3();

        Vector3 l_forward = camera.forward;
        Vector3 l_right = camera.right;

        //var hitRotation = Quaternion.FromToRotation(Vector3.up, Surface());
        //transform.rotation = hitRotation;

        Vector3 surface = Surface();

        l_forward.y = (- (l_forward.x * surface.x) - (l_forward.z * surface.z)) / surface.y; //xa*xb + ya*yb + za*zb = 0
        l_forward.Normalize();

        l_right.y = 0.0f;
        l_right.Normalize();


        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) l_Movement += l_forward;
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) l_Movement -= l_forward;
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) l_Movement += l_right;
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) l_Movement -= l_right;

        
        l_Movement.Normalize();

        bool l_isMoving = l_Movement.magnitude > 0.0f;
        
        if(l_isMoving)
        {
            transform.forward = l_Movement;
            //transform.rotation = hitRotation;
        } 


        float currentSpeedMultiplier = 1.0f;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeedMultiplier = speedMultiplier;
        }

        l_Movement *= speed * currentSpeedMultiplier;

        //characterController.Move(l_Movement * Time.fixedDeltaTime);

        float speedParam = (l_Movement.magnitude) / (speed * speedMultiplier);
        animator.SetFloat("Speed", speedParam);
        Jump(l_Movement);
    }

    private void Jump(Vector3 l_Movement)
    {
        float gravity = -2 * mHeightJump * speed * mJumpMultiplier * speed * mJumpMultiplier / (mHalfLengthJump * mHalfLengthJump);
        if (mVerticalSpeed < 0) gravity *= mDownGravityMultiplier;
        mVerticalSpeed += gravity * Time.fixedDeltaTime;
        l_Movement.y = mVerticalSpeed * Time.fixedDeltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime;

        CollisionFlags colls = characterController.Move(l_Movement * Time.fixedDeltaTime);
        //mOnGround = (colls & CollisionFlags.Below) != 0;
        mOnGround = isGrounded();
        //mContactCeiling = (colls & CollisionFlags.Above) != 0;

        if (mOnGround) mVerticalSpeed = 0.0f;
        //if (mContactCeiling && mVerticalSpeed > 0.0f) mVerticalSpeed = 0.0f;

        if (mDoJump)
        {
            mVerticalSpeed = 2 * mHeightJump * speed * mJumpMultiplier / mHalfLengthJump;
            mDoJump = false;
        }
    }

    private bool CanJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !mOnGround && extraJumps > 0)
        {
            extraJumps--;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && mOnGround)
        {
            extraJumps = maxExtraJumps;
            return true;
        }
        return false;
    }

    private bool isGrounded()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));

        return Physics.Raycast(ray,out RaycastHit hit, groundThreshold, layerMask);
    }

    private Vector3 Surface()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        
        if(Physics.Raycast(ray,out RaycastHit hit, 5.0f, layerMask))
            return hit.normal;
        
        return Vector3.up;
    }
}
