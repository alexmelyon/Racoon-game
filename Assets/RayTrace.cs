using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RayTrace : MonoBehaviour
{
    // Жертва и путь
    public GameObject victim;

    // Система охоты
    private bool IsHunting = false;
    private bool LostVictim = false;
    private Vector3 LastSeenPosition;
    
    // Хвост, нос
    private GameObject tail, nose;

    // Start is called before the first frame update
    void Start()
    {
        nose = gameObject.GetComponentsInChildren<GameObject>()[0];
        tail = gameObject.GetComponentsInChildren<GameObject>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit NoseHit, TailHit; string VictimName = "Enot";
        if (Physics.Raycast(nose.transform.position, victim.transform.position - nose.transform.position, out NoseHit, 50.0f))
            if (NoseHit.collider.name == VictimName)         
                if (Physics.Raycast(tail.transform.position, victim.transform.position - tail.transform.position, out TailHit, 50.0f))
                    if (TailHit.collider.name == VictimName)
                        if (NoseHit.distance < TailHit.distance)
                            IfVisible(); // Если видит
                        else IfInvisible();
                    else IfVisible(); // Если видит только носом   
            else IfInvisible();

        if (IsHunting)
            FollowVictim();
        else
            FollowPath();
    }

    void IfVisible()
    {
        this.gameObject.GetComponent<Light>().color = Color.green;
        IsHunting = true;
        LastSeenPosition = victim.transform.position;
    }

    void IfInvisible()
    {
        this.gameObject.GetComponent<Light>().color = Color.red;
        if (LostVictim)
            ReturnToPath();
    }

    void FollowVictim()
    {
        Debug.Log("FOLLOW");
        // Здесь чтобы следовала за ним в LastSeenPosition
    }

    void ReturnToPath()
    {
        IsHunting = false;
    }

    void FollowPath()
    {
        Debug.Log("PATROL");
    }
}