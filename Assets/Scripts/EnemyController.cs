using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    public LayerMask groundlayer, playerlayer;

    Rigidbody enemy;
    public Vector3 walkpoint;

    bool walkpointset = false;

    public float walkPointRange;

    public bool playerInSightRange, playerInAttackRange;
    public float sightRange, attackRange;

    public GameObject projectile;

    public GameObject projectilePosition;

    public float timeBetweenAttacks;
    
    bool alreadyAttacked;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange, playerlayer);

        if (playerInSightRange && playerInAttackRange) Attack();

        if(!playerInSightRange && playerInAttackRange) Patrol();

        // if(playerInSightRange &&)
    }

    private void Patrol()
    {
        if(!walkpointset)SearchWalkPoint();
        
        if(walkpointset)
        {
            agent.SetDestination(walkpoint);
        }
        Vector3 distanceWalkPoint = transform.position - walkpoint;

        if(distanceWalkPoint.magnitude < 1f)
        {
            walkpointset = false;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange,walkPointRange);
        float randomY = Random.Range(-walkPointRange,walkPointRange);

        walkpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if(Physics.Raycast(walkpoint,-transform.up,1f,groundlayer))
        {
            walkpointset = true;
        }
    }

    public void Attack()
    {
        transform.LookAt(player);

        if(alreadyAttacked)
        {
            if(!alreadyAttacked)
            {
                Rigidbody rb = Instantiate(projectile,projectilePosition.transform.position,Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f,ForceMode.Impulse);
                rb.AddForce(transform.up * 32f,ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
