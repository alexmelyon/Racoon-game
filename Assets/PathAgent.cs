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
        // Vector3 pos = Vector3.MoveTowards(transform.position, next, speed * Time.deltaTime);
        // transform.position = pos;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        // Debug.Log("DESTINATION");
        agent.SetDestination(next);
        
        
        float magnitude = Vector3.Magnitude(next - transform.position);
        // Debug.Log("DESTINATION " + magnitude);
        if(magnitude < 1.5) {
            pathCreator.RemoveNext();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Dog>() != null) {
            Debug.Log("FAIL");
            if(LevelLoader.Instance != null) {
                LevelLoader.Instance.OnFail();
            }
        }
    }
}
