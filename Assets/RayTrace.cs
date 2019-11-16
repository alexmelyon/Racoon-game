using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RayTrace : MonoBehaviour
{
    public enum HunterState { Roaming, Hunting }

    // Жертва и путь
    public GameObject victim;
    public HunterState state;

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

        if (IsHunting && state != HunterState.Hunting)
            FollowVictim();
        else if (!IsHunting && state != HunterState.Roaming)
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
    void ReturnToPath()
    {
        IsHunting = false;
        // Тут возвращаемся на путь
    }

    void FollowVictim()
    {
        state = HunterState.Hunting;
        // Здесь чтобы следовала за ним в LastSeenPosition
    }

    void FollowPath()
    {
        state = HunterState.Roaming;
        // Здесь чтобы отправлялся следовать по пути
    }
}
