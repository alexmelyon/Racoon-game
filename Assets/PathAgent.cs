using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 100F;


    // Update is called once per frame
    void Update()
    {
        if(pathCreator.IsEmpty()) {
            return;
        }
        Vector3 next = pathCreator.GetNext();
        Vector3 pos = Vector3.MoveTowards(transform.position, next, speed * Time.deltaTime);
        transform.position = pos;
        if(Vector3.Magnitude(next - transform.position) < 1) {
            pathCreator.RemoveNext();
        }
    }
}
