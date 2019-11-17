    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTrace : MonoBehaviour
{
    public enum HunterState { Roaming, Hunting }

    // Жертва и путь
    public GameObject victim;
    public HunterState state;
    public bool isVictimVisible = false;

    // Система охоты
    private bool IsHunting = false;
    private bool LostVictim = false;
    public Vector3 LastSeenPosition;

    // Хвост, нос
    public GameObject tail, nose;

    // Start is called before the first frame update
    void Start()
    {
        // nose = gameObject.GetComponentsInChildren<GameObject>()[0];
        // tail = gameObject.GetComponentsInChildren<GameObject>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit NoseHit, TailHit;
        float maxDistance = 50F;
        if (Physics.Raycast(nose.transform.position, victim.transform.position - nose.transform.position, out NoseHit, maxDistance))
        {
            Debug.Log("NOSE " + NoseHit.collider.gameObject.name);
            if(NoseHit.collider.gameObject.transform.IsChildOf(victim.transform))
            {
                if (Physics.Raycast(tail.transform.position, victim.transform.position - tail.transform.position, out TailHit, maxDistance))
                {
                    // if (TailHit.collider.gameObject == victim)
                    if (TailHit.collider.gameObject.transform.IsChildOf(victim.transform))
                    {
                        if (NoseHit.distance < (TailHit.distance - Vector3.Magnitude(nose.transform.position - tail.transform.position) / 4))
                        {
                            IfVisible(); // Если видит
                        }
                        else
                        {
                            IfInvisible();
                        }
                    }
                    else IfVisible(); // Если видит только носом   
                }
                else
                {
                    IfInvisible();
                }
            }
        }
        if (IsHunting && state != HunterState.Hunting)
            FollowVictim();
        else if (!IsHunting && state != HunterState.Roaming)
            FollowPath();

        // RaycastHit hit;
        // Vector3 dir = victim.transform.position - transform.position;
        // // Debug.Log("RAYCAST");
        // if(Physics.Raycast(transform.position, dir, out hit)) {
        //     Debug.Log("RAYCAST 2 " + hit.collider.gameObject.name);
        //     if(hit.collider.gameObject.transform.IsChildOf(victim.transform)) {
        //     // if(hit.collider.gameObject.name == victim.name) {
        //         Debug.Log("RAYCAST 3");
        //         Vector3 point = hit.point;
        //         Vector3 dir2 = point - transform.position;
        //         float res = Mathf.Atan2(dir2.y, dir2.x);
        //         Debug.Log("RES " + res);
        //     }
        // }
    }

    void IfVisible()
    {
        isVictimVisible = true;
        // this.gameObject.GetComponent<Light>().color = Color.green;
        IsHunting = true;
        LastSeenPosition = victim.transform.position;
    }

    void IfInvisible()
    {
        isVictimVisible = false;
        // this.gameObject.GetComponent<Light>().color = Color.red;
        if (LostVictim)
            FollowPath();
    }

    void FollowVictim()
    {
        Debug.Log("FOLLOW VICTIM");
        state = HunterState.Hunting;
        // Здесь чтобы следовала за ним в LastSeenPosition
        GetComponent<Dog>().DoFollow();
    }

    void FollowPath()
    {
        Debug.Log("FOLLOW PATH");
        state = HunterState.Roaming;
        IsHunting = false;
        LostVictim = false;
        // Здесь чтобы отправлялся следовать по пути
        GetComponent<Dog>().DoPatrol();
    }
    public void LastSeenReached() {
        Debug.Log("LAST SEEN REACHED");
        LostVictim = true;
    }
}
