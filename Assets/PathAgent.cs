using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class PathAgent : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 10F;
    public GameObject floor;

    void Start() {
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        bool warped = agent.Warp(floor.transform.position);
        Debug.Log("START " + warped);

        // StartCoroutine(HoldNavAgent(floor, GetComponent<NavMeshAgent>()));
    }

    // public IEnumerator HoldNavAgent(GameObject target, NavMeshAgent pathFinder)
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     pathFinder.enabled = true;
    //     target = GameObject.FindGameObjectWithTag("NavTarget");
    //     pathFinder.speed = speed;
    //     pathFinder.SetDestination(target.transform.position);
    // }

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
}
