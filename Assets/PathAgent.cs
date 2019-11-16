using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class PathAgent : MonoBehaviour
{
    public PathCreator pathCreator;

    void Start() {
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        bool warped = agent.Warp(transform.position);
    }

    void Update()
    {
        if(pathCreator.IsEmpty()) {
            return;
        }
        Vector3 next = pathCreator.GetNext();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(next);
        
        float magnitude = Vector3.Magnitude(next - transform.position);
        if(magnitude < 1.5) {
            pathCreator.RemoveNext();
        }
    }
}
