using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisitorPathFinding : MonoBehaviour
{
    public GameObject toFollow;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(toFollow.transform.position, transform.position) > 3)
        {
            if (!isWalking)
            {
                animator.SetBool("isWalking", true);
                isWalking = true;
            }
            agent.isStopped = false;
            agent.destination = toFollow.transform.position;
        }
        else
        {
            if (isWalking)
            {
                animator.SetBool("isWalking", false);
                isWalking = false;
            }
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
        }
    }
}
