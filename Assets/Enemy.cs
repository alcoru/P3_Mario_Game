using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    enum EnemyStates { PATROL, CHASE, ATTACK, DIE };
    EnemyStates currentState;
    float timeToAttack = 1.0f;
    [SerializeField] GameObject target;

    [SerializeField] private Vector3[] patrolPositions;
    [SerializeField] private float maxChaseDistance;
    [SerializeField] private float minChaseDistance;

    private int countPatrol = 0;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
            /*case EnemyStates.ATTACK:
                UpdateATTACK();
                break;*/
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
            case EnemyStates.ATTACK:
                StartATTACK();
                break;
        }
    }

    void StartATTACK()
    {
        timeToAttack = 1.0f;
    }

    void UpdatePATROL()
    {
        navMeshAgent.destination = patrolPositions[countPatrol];
        if ((transform.position - patrolPositions[countPatrol]).magnitude < 0.1f)

            if (countPatrol == patrolPositions.Length-1)
                countPatrol = 0;
            else
                countPatrol++;

    }

    void UpdateCHASE()
    {
        if ((transform.position - target.transform.position).magnitude > maxChaseDistance)
            SetState(EnemyStates.PATROL);
        else if ((transform.position - target.transform.position).magnitude > minChaseDistance)
            navMeshAgent.destination = target.transform.position;
        else
            SetState(EnemyStates.ATTACK);
    }

    /*void UpdateATTACK()
    {
        transform.LookAt(target.transform);
        if (timeToAttack <= 0)
        {
            Instantiate(projectile, enemyPointer.position, enemyPointer.rotation);
            timeShoot = startTimeShoot;
        }
        else timeShoot -= Time.deltaTime;

        if ((transform.position - target.transform.position).magnitude > maxShootDistance)
            SetState(EnemyStates.CHASE);
    }*/

    void UpdateDIE()
    {
        Destroy(this);
    }

    void EndPATROL()
    {
        navMeshAgent.destination = transform.position;
    }

    void EndCHASE()
    {
        navMeshAgent.destination = transform.position;
    }
}
