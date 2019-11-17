using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageEater : MonoBehaviour
{
    public Exit exit;

    private int sausageMax = 0;
    private int sausageCount = 0;

    void Start()
    {
        sausageMax = FindObjectsOfType<Sausage>().Length;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER " + other.gameObject.name);
        if(other.gameObject.GetComponent<Sausage>() != null) {
            Debug.Log("SAUSAGE " + sausageCount+"/"+sausageMax);
            sausageCount++;
            if(sausageCount == sausageMax) {
                exit.OpenDoor();
            }
            
            Destroy(other.gameObject);

        } else if(other.gameObject.GetComponent<Exit>() != null) {
            if(sausageCount == sausageMax) {
                Debug.Log("EXIT OPEN");
            } else {
                Debug.Log("EXIT CLOSED");
            }
        }
    }
}
