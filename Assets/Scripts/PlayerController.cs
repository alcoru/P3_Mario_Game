using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    Transform initialPosition;

    public CharacterController characterController;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] Transform camera;

    [SerializeField] float speed = 2.0f;
    [SerializeField] float speedMultiplier = 2.0f;

    public bool mOnGround;
    [SerializeField] float[] mHeightJump = new float[3] { 150f, 175f, 200f };
    [SerializeField] float mHeightLongJump = 175f;
    [SerializeField] float mHalfLengthJump;
    [SerializeField] float mDownGravityMultiplier;
    [SerializeField] float mJumpMultiplier;
    private int extraJumps;
    private int maxExtraJumps = 0;

    float mVerticalSpeed = 0.0f;

    private bool mDoJump = false;
    private bool onAirSpecialJump = false;
    private bool activeMove = true;

    [SerializeField] LayerMask layerMask;

    [SerializeField] float groundThreshold;

    [SerializeField]
    private float initialTimeToJump = 0.2f;
    private float timeToJump = 0f;
    private int jumps = 0;

    Vector3 moveDir;

    [SerializeField] private float specialIdleTime = 10f;
    private float idleTime;


    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        initialPosition = transform;
        idleTime = specialIdleTime;
    }

    void Start()
    {

    }

    void Update()
    {
        if (CanJump()) mDoJump = true;
        UpdateTimeToJump();
        SpecialIdle();

    }

    private void SpecialIdle()
    {
        if (characterController.velocity.magnitude == 0)
        {
            if (idleTime > 0)
                idleTime -= Time.deltaTime;
            else
            {
                animator.SetTrigger("SpecialIdle");
                idleTime = specialIdleTime;
            }
        }
        else
        {
            idleTime = specialIdleTime;
        }
    }

    private void UpdateTimeToJump()
    {
        if (isGrounded())
        {
            if (timeToJump <= 0 || jumps>=mHeightJump.Length)
            {
                jumps = 0;
            }
            else
            {
                timeToJump -= Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 l_Movement = new Vector3();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;

        if (activeMove && dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angleRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angleRotation, 0f);

            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            float currentSpeedMultiplier = 1.0f;
            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeedMultiplier = speedMultiplier;
            
            l_Movement = moveDir.normalized;
            l_Movement *= speed * currentSpeedMultiplier;

            characterController.Move(l_Movement * Time.deltaTime);
        }

        float speedParam = (l_Movement.magnitude) / (speed * speedMultiplier);
        animator.SetFloat("Speed", speedParam);
        Gravity(l_Movement);
    }

    private void Gravity(Vector3 l_Movement)
    {
        float gravity = -2 * mHeightJump[jumps] * speed * mJumpMultiplier * speed * mJumpMultiplier / (mHalfLengthJump * mHalfLengthJump);
        if (mVerticalSpeed < 0 || !Input.GetKey(KeyCode.Space) || onAirSpecialJump) gravity *= mDownGravityMultiplier;
        mVerticalSpeed += gravity * Time.fixedDeltaTime;
        l_Movement.y = mVerticalSpeed * Time.fixedDeltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime;
        animator.SetFloat("VerticalMovement", l_Movement.y);


        CollisionFlags colls = characterController.Move(l_Movement * Time.fixedDeltaTime);
        mOnGround = isGrounded();
        animator.SetBool("OnGround", mOnGround);

        if (mOnGround)
        {
            mVerticalSpeed = 0.0f;
            onAirSpecialJump = false;
        }

        Jump();

    }

    private void Jump()
    {
        if (mDoJump)
        {
            mVerticalSpeed = 2 * mHeightJump[jumps] * speed * mJumpMultiplier / mHalfLengthJump;
            mDoJump = false;
            timeToJump = initialTimeToJump;
            jumps += 1;
            switch (jumps)
            {
                case 1:
                    animator.SetTrigger("Jump");
                    AudioManager.PlaySound("jump1");
                    break;
                case 2:
                    animator.SetTrigger("DoubleJump");
                    AudioManager.PlaySound("jump2");
                    break;
                default:
                    animator.SetTrigger("TripleJump");
                    AudioManager.PlaySound("jump3");
                    break;
            }

        }
    }

    public void doLongJump(float height)
    {
        onAirSpecialJump = true;
        animator.SetTrigger("LongJump");
        AudioManager.PlaySound("long_jump");
        mVerticalSpeed = 2 * height * speed * mJumpMultiplier / mHalfLengthJump;
    }

    public void doWallJump(float height)
    {
        onAirSpecialJump = true;
        animator.SetTrigger("Jump");
        AudioManager.PlaySound("jump1");
        mVerticalSpeed = 2 * height * speed * mJumpMultiplier / mHalfLengthJump;
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

    public bool isGrounded()
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

    public void activateMove(bool active)
    {
        activeMove = active;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.GetComponent<Enemy>())
        {
            if(CanKillWithFeet(hit))
            {
                hit.transform.GetComponent<Enemy>().KillEnemy();
                mVerticalSpeed = 2 * 200.0f * speed * mJumpMultiplier / mHalfLengthJump;
            }
        }

        if(hit.transform.GetComponent<BridgePlayerCollision>())
        {
            hit.transform.GetComponent<BridgePlayerCollision>().AddForce(hit.normal, hit.point);
        }
    }

    bool CanKillWithFeet(ControllerColliderHit enemy)
    {
        return enemy.normal.y > 0.5;
    }
}
