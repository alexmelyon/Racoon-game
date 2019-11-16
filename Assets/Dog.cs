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

    public GameObject[] patrolDots;
    
    private int lastPatrolDot = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Vector3 next = patrolDots[lastPatrolDot].transform.position;
        agent.SetDestination(next);
        if(dogState == DogState.PATROL_FORWARD) {
            if(Vector3.Magnitude(next - transform.position) < 1.5) {
                lastPatrolDot++;
            }
            if(lastPatrolDot == patrolDots.Length - 1) {
                dogState = DogState.PATROL_BACKWARD;
            }
        } else if(dogState == DogState.PATROL_BACKWARD) {
            if(Vector3.Magnitude(next - transform.position) < 1.5) {
                lastPatrolDot--;
            }
            if(lastPatrolDot == 0) {
                dogState = DogState.PATROL_FORWARD;
            }
        } else if(dogState == DogState.FOLLOW_VICTIM) {
            Debug.Log("FOLLOW VICTIM");
        }
    }
}
