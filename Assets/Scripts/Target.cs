using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public Transform pos;
    public Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(pos.position.x, terrain.SampleHeight(transform.position), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
