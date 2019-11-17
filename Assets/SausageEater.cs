using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageEater : MonoBehaviour
{

    private int sausageCount = 0;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER " + other.gameObject.name);
        if(other.gameObject.GetComponent<Sausage>() != null) {
            Debug.Log("SAUSAGE");
            sausageCount++;
            

        } else if(other.gameObject.GetComponent<Exit>() != null) {
            Debug.Log("EXIT");

        }
    }
}
