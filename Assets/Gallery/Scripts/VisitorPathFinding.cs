using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisitorPathFinding : MonoBehaviour
{
    public GameObject toFollow;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(toFollow.transform.position, transform.position) > 3)
        {
            agent.isStopped = false;
            agent.destination = toFollow.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
