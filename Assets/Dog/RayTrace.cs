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
    public GameObject nose;
    public float fov = 90;

    // Start is called before the first frame update
    void Start()
    {
        victim = FindObjectOfType<PathAgent>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 victimDir = victim.transform.position - transform.position;
        if(Physics.Raycast(transform.position, victimDir, out hit)) {
            if(hit.collider.gameObject.transform.IsChildOf(victim.transform)) {
                Vector3 point = hit.point;
                Vector3 noseDir = nose.transform.position - transform.position;
                float res = Vector3.Angle(victimDir, noseDir);
                if(Mathf.Round(res) < fov / 2) {
                    IfVisible();
                } else {
                    IfInvisible();
                }
            } else IfInvisible();
        } else IfInvisible();
        
        if (IsHunting && state != HunterState.Hunting)
            FollowVictim();
        else if (!IsHunting && state != HunterState.Roaming)
            FollowPath();
    }

    void IfVisible()
    {
        isVictimVisible = true;
        IsHunting = true;
        LastSeenPosition = victim.transform.position;
    }

    void IfInvisible()
    {
        isVictimVisible = false;
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
