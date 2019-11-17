using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour
{
    public Teleport another;

    public GameObject exit;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PathAgent>() != null) {
            other.gameObject.transform.position = another.exit.transform.position;
            other.gameObject.GetComponent<NavMeshAgent>().SetDestination(another.exit.transform.position);
        }
    }
}
