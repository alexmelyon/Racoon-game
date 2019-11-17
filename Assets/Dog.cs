using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    enum DogState {
        PATROL_FORWARD,
        PATROL_BACKWARD,
        FOLLOW_VICTIM
    }
    DogState dogState = DogState.PATROL_FORWARD;

    public GameObject victim;
    public float walkSpeed = 3.5F;
    public float runSpeed = 5F;
    public GameObject[] patrolDots;
    public GameObject walkDog;
    public GameObject runDog;
    
    private int lastPatrolIndex = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Vector3 next = patrolDots[lastPatrolIndex].transform.position;
        agent.SetDestination(next);
        if(dogState == DogState.PATROL_FORWARD) {
            agent.speed = walkSpeed;
            if(Vector3.Magnitude(next - transform.position) < 1.5) {
                lastPatrolIndex++;
            }
            if(lastPatrolIndex == patrolDots.Length - 1) {
                dogState = DogState.PATROL_BACKWARD;
            }
        } else if(dogState == DogState.PATROL_BACKWARD) {
            agent.speed = walkSpeed;
            if(Vector3.Magnitude(next - transform.position) < 1.5) {
                lastPatrolIndex--;
            }
            if(lastPatrolIndex == 0) {
                dogState = DogState.PATROL_FORWARD;
            }
        } else if(dogState == DogState.FOLLOW_VICTIM) {
            agent.speed = runSpeed;
            RayTrace rt = GetComponent<RayTrace>();
            if(rt.isVictimVisible) {
                agent.SetDestination(victim.transform.position);
            } else {
                agent.SetDestination(rt.LastSeenPosition);
            }
        }
    }

    public void DoFollow() {
        dogState = DogState.FOLLOW_VICTIM;
        runDog.SetActive(true);
        walkDog.SetActive(false);
    }

    public void DoPatrol()
    {
        dogState = DogState.PATROL_FORWARD;
        walkDog.SetActive(true);
        runDog.SetActive(false);
    }

    public void LastSeenReached() {
        GetComponent<RayTrace>().LastSeenReached();
    }
}
