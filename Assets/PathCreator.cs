using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    public GameObject pathPrefab;
    class PathDot {
        public Vector3 pos;
        public GameObject go;
    }
    List<PathDot> pathList = new List<PathDot>();

    // Update is called once per frame
    void Update()
    {
        // for (Touch touch : Touch in Input.touches) {
        //     if (touch.phase == TouchPhase.Began) {
        //         // Construct a ray from the current touch coordinates
        //         var ray = Camera.main.ScreenPointToRay (touch.position);
        //         if (Physics.Raycast (ray)) {
        //             // Create a particle if hit
        //             Instantiate (particle, transform.position, transform.rotation);
        //         }
        //     }
        // }
    
        if(Input.GetMouseButton(0)) {
            Vector3 mouse = Input.mousePosition;
            Debug.Log("X " + mouse.x + " Y " + mouse.y);
            RaycastHit hit;
            bool r = Physics.Raycast(Camera.main.transform.position, mouse, out hit, 100F, 0);
            if(r) {
                
            }
            
            PathDot dot = new PathDot();
            dot.pos = mouse;
            GameObject go = Instantiate(pathPrefab, mouse, Quaternion.identity);
            // Destroy(go);
            dot.go = go;
            pathList.Add(dot);
        }
    }

    public bool IsEmpty() {
        return pathList.Count == 0;
    }

    public Vector3 GetNext() {
        return pathList[0].pos;
    }

    public void RemoveNext() {
        GameObject go = pathList[0].go;
        // Debug.Log(go);
        Destroy(go);
        pathList.RemoveAt(0);
    }
}
