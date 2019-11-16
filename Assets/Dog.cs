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
    public GameObject[] patrolDots;
    
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
            if(Vector3.Magnitude(next - transform.position) < 1.5) {
                lastPatrolIndex++;
            }
            if(lastPatrolIndex == patrolDots.Length - 1) {
                dogState = DogState.PATROL_BACKWARD;
            }
        } else if(dogState == DogState.PATROL_BACKWARD) {
            if(Vector3.Magnitude(next - transform.position) < 1.5) {
                lastPatrolIndex--;
            }
            if(lastPatrolIndex == 0) {
                dogState = DogState.PATROL_FORWARD;
            }
        } else if(dogState == DogState.FOLLOW_VICTIM) {
            RayTrace rt = GetComponent<RayTrace>();
            if(rt.isVictimVisible) {
                Debug.Log("FOLLOW VICTIM 1");
                agent.SetDestination(victim.transform.position);
            } else {
                Debug.Log("FOLLOW VICTIM 2");
                agent.SetDestination(rt.LastSeenPosition);
            }
        }
    }

    public void DoFollow() {
        dogState = DogState.FOLLOW_VICTIM;
    }

    public void DoPatrol()
    {
        dogState = DogState.PATROL_FORWARD;
    }

    public void LastSeenReached() {
        GetComponent<RayTrace>().LastSeenReached();
    }
}
