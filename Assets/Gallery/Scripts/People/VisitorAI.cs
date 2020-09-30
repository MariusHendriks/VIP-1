using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;

public class VisitorAI : MonoBehaviour
{
    public List<GameObject> artObjects;
    public float interestedTime = 6f;
    public GameObject forceTarget;


    private GameObject target;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isWalking;
    private float timer = 0f;
    private bool activeActivity = false;
    private string currentActivity;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        target = gameObject;
        GetTarget();

    }



    // Update is called once per frame
    void Update()
    {

        var targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        if (Vector3.Distance(targetPos, transform.position) > target.GetComponent<ArtpieceProps>().inspectionDistance)
        {
            if (!isWalking)
            {
                animator.SetBool("isWalking", true);
                isWalking = true;
            }
            agent.isStopped = false;
            agent.destination = target.transform.position;
        }
        else
        {
            //AI is at the target
            if (isWalking)
            {
                animator.SetBool("isWalking", false);
                isWalking = false;
            }
            agent.isStopped = true;


            PlayActivity("isInterested");


        }

        if (activeActivity)
        {
            timer += Time.deltaTime;

            if (timer > interestedTime)
            {
                EndActivity();
            }
        }


    }
    private void PlayActivity(string activity)
    {
        var targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(targetPos);
        currentActivity = activity;
        animator.SetBool("isWalking", false);
        animator.SetBool(activity, true);
        activeActivity = true;

    }

    private void EndActivity()
    {
        animator.SetBool(currentActivity, false);
        GetTarget();
        timer = 0;
        activeActivity = false;
    }

    private void GetTarget()
    {
        if (forceTarget != null)
        {
            target = forceTarget;
        }
        else
        {
            Transform artpiece = artObjects[Random.Range(0, artObjects.Count)].transform;
            target = artpiece.GetChild(Random.Range(0, artpiece.childCount)).gameObject;
        }

    }
}
