using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageEater : MonoBehaviour
{
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Sausage>() != null) {
            Debug.Log("SAUSAGE");   
            // FixedJoint joint = GetComponent<FixedJoint>();
            // joint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
        }
    }
}
