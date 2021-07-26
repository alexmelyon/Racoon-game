using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DogPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        children.ToList().ForEach(c => c.gameObject.SetActive(false));
    }

}
