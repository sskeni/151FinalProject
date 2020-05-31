using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeTest : MonoBehaviour
{
    public GameObject hinge;
    // Update is called once per frame
    void Update()
    {
        transform.position = hinge.transform.position;
    }
}
