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
    public bool forceTargetEnabled = false;

    //tidelijk
    public GameObject target;


    private NavMeshAgent agent;
    private Animator animator;
    private bool isWalking;
    private float timer = 0f;
    private bool activeActivity = false;
    private string currentActivity;

    private string[] artpieceTags = new string[] { "AIInteractable", "AIViewable" };

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

        if (!target.GetComponent<ArtpieceProps>())
        {
            AddArtpieceProps(target);
        }

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

    private void AddArtpieceProps(GameObject target)
    {
        target.AddComponent<ArtpieceProps>();
    }

    private void GrabObject(GameObject target)
    {

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
        if (forceTarget != null && forceTargetEnabled)
        {
            target = forceTarget;
        }
        else
        {
            Transform artpiece = artObjects[Random.Range(0, artObjects.Count)].transform;
            List<GameObject> childrenArtPieces = new List<GameObject>();
            for (int i = 0; i < artpiece.childCount; i++)
            {
                Transform child = artpiece.GetChild(i);
                if (artpieceTags.Contains(child.tag))
                {
                    childrenArtPieces.Add(child.gameObject);
                }
            }
            if(childrenArtPieces.Count > 0)
            {
                target = childrenArtPieces[Random.Range(0, childrenArtPieces.Count)];
            }
        }

    }
}
