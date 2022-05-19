using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDino : MonoBehaviour
{
    //default speed
    float speed = 50.0f;

    // Start is called before the first frame update
    void Start() => transform.position = new Vector3(0f, 0f, 0f);

    // Update is called once per frame
    void Update()
    {

        //default fly
        transform.position += transform.forward * Time.deltaTime * speed;
        // slow down when up, faster when goes down
        speed -= transform.forward.y * Time.deltaTime * 50.0f;
        //minimum speed
        if(speed < 30.0f)
        {
            speed = 30.0f;
        }
        //camera movement
        Vector3 camera = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f;
        Camera.main.transform.position = camera;
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);


        //rotate right/ left, up/down
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -1 * Input.GetAxis("Horizontal"));
        //position of the terrain
        float now = Terrain.activeTerrain.SampleHeight(transform.position);
        //border for the terrain
        if(now > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, now, transform.position.z);
        }
    }
}
