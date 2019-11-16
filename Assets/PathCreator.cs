using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    public GameObject pathPrefab;
    class PathDot
    {
        public Vector3 pos;
        public GameObject go;
    }
    List<PathDot> pathList = new List<PathDot>();
    private GameObject lastCreated;

    // Update is called once per frame
    void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                handleTouch(touch.position);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouse = Input.mousePosition;
            handleTouch(mouse);
        }
    }

    void handleTouch(Vector3 screePos)
    {
        var ray = Camera.main.ScreenPointToRay(screePos);
        RaycastHit hit;
        Debug.Log("GROUND " + LayerMask.NameToLayer("Ground"));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 worldPos = hit.point;

            float minDistance = 1;
            GameObject go = null;
            if (lastCreated == null || lastCreated != null && Vector3.Magnitude(lastCreated.transform.position - worldPos) > minDistance)
            {
                go = Instantiate(pathPrefab, worldPos, Quaternion.identity);
                lastCreated = go;
            }
            PathDot dot = new PathDot();
            dot.go = go;
            dot.pos = worldPos;
            pathList.Add(dot);
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
