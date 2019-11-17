using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PathCreator : MonoBehaviour
{
    public float distanceForDot = 3F;
    public GameObject pathPrefab;
    public NavMeshAgent racoon;
    class PathDot
    {
        public Vector3 pos;
        public GameObject go;
    }
    List<PathDot> pathList = new List<PathDot>();
    List<PathDot> tempPathList = new List<PathDot>();
    private GameObject lastCreated;

    // Update is called once per frame
    void Update()
    {
        foreach (var touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began) {
                ClearPath();
            }
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                handleTouch(touch.position);
            }
            if(touch.phase == TouchPhase.Ended) {
                pathList = tempPathList;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if(Input.GetMouseButtonDown(0)) {
                ClearPath();
            }
            Vector3 mouse = Input.mousePosition;
            handleTouch(mouse);
        }
        if(Input.GetMouseButtonUp(0)) {
            Debug.Log("RELEASE");
            replacePath();
        }
    }

    void replacePath() {
        pathList.AddRange(tempPathList);
        tempPathList.Clear();
    }

    void ClearPath() {
        NavMeshAgent agent = racoon;
        if(!agent.isStopped) {
            agent.SetDestination(agent.transform.position);
        }
        foreach(PathDot d in pathList) {
            if(d.go != null) {
                Destroy(d.go);
            }
        }
        pathList.Clear();
    }

    void handleTouch(Vector3 screePos)
    {
        var ray = Camera.main.ScreenPointToRay(screePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 worldPos = hit.point;

            
            // if(Vector3.Magnitude(worldPos - racoon.transform.position) > distanceForDot)

            float minDistance = 1;
            GameObject go = null;
            if (lastCreated == null || lastCreated != null && Vector3.Magnitude(lastCreated.transform.position - worldPos) > minDistance)
            {
                go = Instantiate(pathPrefab, worldPos, Quaternion.identity);
                lastCreated = go;
                
                PathDot dot = new PathDot();
                dot.go = go;
                dot.pos = worldPos;
                tempPathList.Add(dot);
            }
        }
    }

    public bool IsEmpty()
    {
        return pathList.Count == 0;
    }

    public Vector3 GetNext()
    {
        return pathList[0].pos;
    }

    public void RemoveNext()
    {
        GameObject go = pathList[0].go;
        if(go != null) {
            Destroy(go);
        }
        pathList.RemoveAt(0);
    }
}
