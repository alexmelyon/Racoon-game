using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour
{
    public Teleport another;

    public GameObject exit;

    private NavMeshAgent agent;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PathAgent>() != null) {
            other.gameObject.transform.position = another.exit.transform.position;
            agent = other.gameObject.GetComponent<NavMeshAgent>();
            agent.SetDestination(another.exit.transform.position);
            StartCoroutine(clearAgent());
        }
    }

    IEnumerator clearAgent() {
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(0.1F);
        agent.SetDestination(agent.destination);
    }
}
