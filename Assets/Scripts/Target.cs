using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform pos;
    public Terrain terrain;

    void Start()
    {
        // just set targets on terrain
        transform.position = new Vector3(pos.position.x, terrain.SampleHeight(transform.position), transform.position.z);
    }
}
