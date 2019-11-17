using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class PathAgent : MonoBehaviour
{
    public PathCreator pathCreator;
    public float runSpeed = 1F;
    public GameObject idleRacoon;
    public GameObject runRacoon;


    void Start() {
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        bool warped = agent.Warp(transform.position);
    }

    void Update()
    {
        
        handleSpeed();
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

    void handleSpeed() {
        float v = GetComponent<NavMeshAgent>().velocity.magnitude;
        Debug.Log("VELOCITY " + v);
        if(v > runSpeed) {
            runRacoon.SetActive(true);
            idleRacoon.SetActive(false);
        } else {
            runRacoon.SetActive(false);
            idleRacoon.SetActive(true);
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
