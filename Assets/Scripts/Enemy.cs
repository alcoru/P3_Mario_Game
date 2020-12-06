using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    enum EnemyStates { PATROL, CHASE, ATTACK, DIE };
    EnemyStates currentState;
    float timeToAttack;
    [SerializeField] GameObject target;

    [SerializeField] private Vector3[] patrolPositions;
    [SerializeField] private float attackRate;
    [SerializeField] private float maxChaseDistance;
    [SerializeField] private float minChaseDistance;
    [SerializeField] private float maxSeeingDistance;
    [SerializeField] private float maxAttackDistance;
    [SerializeField] Transform enemyPointer;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float actionRadius;
    [SerializeField] GameObject dropItem;

    private int countPatrol = 0;
    private Animator animator;

    private float walkingSpeed = 3.5f;
    private float runningSpeed = 5f;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.PATROL:
                UpdatePATROL();
                break;
            case EnemyStates.CHASE:
                UpdateCHASE();
                break;
            case EnemyStates.ATTACK:
                UpdateATTACK();
                break;
            case EnemyStates.DIE:
                UpdateDIE();
                break;
        }
    }

    private void SetState(EnemyStates newState)
    {
        switch (currentState)
        {
            case EnemyStates.PATROL:
                EndPATROL();
                break;
            case EnemyStates.CHASE:
                EndCHASE();
                break;
        }
        currentState = newState;
        switch (currentState)
        {
            case EnemyStates.PATROL:              
                animator.SetBool("Chasing", false);
                navMeshAgent.speed = walkingSpeed;
                break;
            case EnemyStates.CHASE:
                animator.SetTrigger("PlayerDetected");
                animator.SetBool("Chasing", true);
                navMeshAgent.speed = runningSpeed;
                break;
            case EnemyStates.ATTACK:
                StartATTACK();
                break;
        }
    }

    void StartATTACK()
    {
        timeToAttack = 0;
    }

    void UpdatePATROL()
    {
        navMeshAgent.destination = patrolPositions[countPatrol];
        if ((transform.position - patrolPositions[countPatrol]).magnitude < 0.5f)
            if (countPatrol == patrolPositions.Length-1)
                countPatrol = 0;
            else
                countPatrol++;

        if ((transform.position - target.transform.position).magnitude < actionRadius)
        {
            if (target.transform.gameObject.tag == "Player")
            {  
                SetState(EnemyStates.CHASE);
            }
        }

    }

    void UpdateCHASE()
    {
        if ((transform.position - target.transform.position).magnitude > maxChaseDistance)
        {
            SetState(EnemyStates.PATROL);
        }
        else if ((transform.position - target.transform.position).magnitude > minChaseDistance)
        {
            navMeshAgent.destination = target.transform.position;
        }
        else
            SetState(EnemyStates.ATTACK);
    }

    void UpdateATTACK()
    {
        transform.LookAt(target.transform);
        if (timeToAttack <= 0)
        {
            timeToAttack = attackRate;
            HealthSystem healthBar = target.GetComponent<HealthSystem>();
            if(healthBar != null)
                healthBar.DealDamage();
        }
        else timeToAttack -= Time.deltaTime;

        if ((transform.position - target.transform.position).magnitude > maxAttackDistance)
        {
            SetState(EnemyStates.CHASE);
        }

    }

    void UpdateDIE()
    {
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position+Vector3.up, transform.rotation);
        }
        Destroy(gameObject);
    }

    void EndPATROL()
    {
        navMeshAgent.destination = transform.position;
    }

    void EndCHASE()
    {
        navMeshAgent.destination = transform.position;
    }

    public void KillEnemy()
    {
        AudioManager.PlaySound("goombaDead");
        SetState(EnemyStates.DIE);
    }
}
