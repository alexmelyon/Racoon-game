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
        if (Physics.Raycast(nose.transform.position, victim.transform.position - nose.transform.position, out NoseHit, 50.0f))
            if (NoseHit.collider.gameObject == victim)
                if (Physics.Raycast(tail.transform.position, victim.transform.position - tail.transform.position, out TailHit, 50.0f))
                    if (TailHit.collider.gameObject == victim)
                        if (NoseHit.distance < TailHit.distance)
                            IfVisible(); // Если видит
                        else IfInvisible();
                    else IfVisible(); // Если видит только носом   
                else IfInvisible();

        if (IsHunting && state != HunterState.Hunting)
            FollowVictim();
        else if (!IsHunting && state != HunterState.Roaming)
            FollowPath();
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
            ReturnToPath();
    }
    void ReturnToPath()
    {
        Debug.Log("RETURN");
        IsHunting = false;
        // Тут возвращаемся на путь
    }

    void FollowVictim()
    {
        Debug.Log("FOLLOW VICTIM");
        state = HunterState.Hunting;
        // Здесь чтобы следовала за ним в LastSeenPosition
    }

    void FollowPath()
    {
        Debug.Log("FOLLOW PATH");
        state = HunterState.Roaming;
        // Здесь чтобы отправлялся следовать по пути
    }
    public void LastSeenReached() {
        Debug.Log("LAST SEEN REACHED");
    }
}
