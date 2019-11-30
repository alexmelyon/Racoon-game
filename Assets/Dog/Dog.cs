using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    enum DogState {
        PATROL_FORWARD,
        PATROL_BACKWARD,
        FOLLOW_VISIBLE_VICTIM,
        FOLLOW_LAST_SEEN_VICTIM_POSITION
    }
    private DogState _dogState = DogState.PATROL_FORWARD;
    DogState dogState {
        get { return _dogState; }
        set { if(_dogState != value) {
            Debug.Log("DOG STATE " + value);
            _dogState = value;
        }}
    }

    public GameObject victim;
    public float walkSpeed = 3.5F;
    public float runSpeed = 5F;
    public GameObject[] patrolDots;
    public GameObject walkDog;
    public GameObject runDog;
    public AudioSource alertSound;
    GameObject nose;
    
    private int lastPatrolIndex = 0;
    private Vector3 lastRacoonPosition;
    private bool reachedLastVisiblePosition = false;

    void Start()
    {
        if(nose == null) {
            GameObject n = transform.Find("Nose").gameObject;
            if(n == null) {
                throw new System.Exception("Nose not found");
            }
            nose = n;
        }
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Vector3 next = patrolDots[lastPatrolIndex].transform.position;
        agent.SetDestination(next);
        if(dogState == DogState.PATROL_FORWARD) {
            agent.speed = walkSpeed;
            if(Vector3.Magnitude(next - transform.position) < 1.5) {
                lastPatrolIndex++;
            }
            if(lastPatrolIndex == patrolDots.Length - 1) {
                dogState = DogState.PATROL_BACKWARD;
            }
            if(IsRacoonVisible()) {
                dogState = DogState.FOLLOW_VISIBLE_VICTIM;
            }
        } else if(dogState == DogState.PATROL_BACKWARD) {
            agent.speed = walkSpeed;
            if(Vector3.Magnitude(next - transform.position) < 1.5) {
                lastPatrolIndex--;
            }
            if(lastPatrolIndex == 0) {
                dogState = DogState.PATROL_FORWARD;
            }
            if(IsRacoonVisible()) {
                dogState = DogState.FOLLOW_VISIBLE_VICTIM;
            }
        } else if(dogState == DogState.FOLLOW_VISIBLE_VICTIM) {
            agent.speed = runSpeed;
            if(IsRacoonVisible()) {
                agent.SetDestination(victim.transform.position);
            } else {
                lastRacoonPosition = victim.transform.position;
                dogState = DogState.FOLLOW_LAST_SEEN_VICTIM_POSITION;
            }
        } else if(dogState == DogState.FOLLOW_LAST_SEEN_VICTIM_POSITION) {
            agent.speed = runSpeed;
            if(IsRacoonVisible()) {
                dogState = DogState.FOLLOW_VISIBLE_VICTIM;
            } else {
                reachedLastVisiblePosition = Vector3.Magnitude(lastRacoonPosition - transform.position) < GetComponent<NavMeshAgent>().stoppingDistance;
                if(!reachedLastVisiblePosition) {
                    agent.SetDestination(lastRacoonPosition);
                } else {
                    dogState = DogState.PATROL_FORWARD;
                }
            }
        }
    }

    public void DoFollow() {
        dogState = DogState.FOLLOW_VISIBLE_VICTIM;
        runDog.SetActive(true);
        walkDog.SetActive(false);
        
        // alertSound.Play();
    }

    public void DoPatrol()
    {
        dogState = DogState.PATROL_FORWARD;
        walkDog.SetActive(true);
        runDog.SetActive(false);
    }

    float fov = 90F;
    bool IsRacoonVisible() {
        
        RaycastHit hit;
        Vector3 victimDir = victim.transform.position - transform.position;
        if(!Physics.Raycast(transform.position, victimDir, out hit)) {
            return false;
        }
        if(!hit.collider.gameObject.transform.IsChildOf(victim.transform)) {
            return false;
        }
        Vector3 point = hit.point;
        Vector3 noseDir = nose.transform.position - transform.position;
        float res = Vector3.Angle(victimDir, noseDir);
        if(Mathf.Abs(Mathf.Round(res)) < fov / 2) {
            return true;
        } else {
            return false;
        }
    }
}
