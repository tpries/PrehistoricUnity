using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDino : MonoBehaviour
{
    //speed
    public float straight = 50.0f, side = 25.0f, ascend = 25.0f;
    private float forward, upward, sideward;

    //acceleration
    private float forward_acc = 20.0f, side_acc = 10.0f, ascend_acc = 10.0f;

    //mouse control, rotation
    public float rotatespeed = 50.0f;
    private Vector2 lookInput, center, mouse_distance;

    private float roll;
    public float roll_speed = 50f, roll_acc = 7.0f;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(500f, 0f, 500f);

        //instantiate the center of the screen
        center.x = Screen.width * 0.5f;
        center.y = Screen.height * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
 

        //check where the mouse is
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        //check for the distance from the center
        mouse_distance.x = (lookInput.x - center.x) / center.x;
        mouse_distance.y = (lookInput.y - center.y) / center.y;

        mouse_distance = Vector2.ClampMagnitude(mouse_distance, .5f);

        //rolling through space
        roll = Mathf.Lerp(roll, Input.GetAxisRaw("Roll"), roll_acc * Time.deltaTime);


        //rotation
        transform.Rotate(-mouse_distance.y * rotatespeed * Time.deltaTime, mouse_distance.x * rotatespeed * Time.deltaTime, roll * roll_speed * Time.deltaTime, Space.Self);


        //speed instantiation
        forward = Mathf.Lerp(forward, Input.GetAxisRaw("Vertical") * straight, forward_acc * Time.deltaTime);
        //sideward = Mathf.Lerp(sideward, Input.GetAxisRaw("Horizontal") * side, side_acc * Time.deltaTime);
        // upward = Mathf.Lerp(upward, Input.GetAxisRaw("Hover") * ascend, ascend_acc * Time.deltaTime);

        //speed up when going down/ slower when ascending
        straight -= transform.forward.y * Time.deltaTime * 20.0f;
        //minimal speed
        if (straight < 25.0f)
        {
            straight = 25.0f;
        }

        //the actual movement
        transform.position += transform.forward * forward * Time.deltaTime;
        transform.position += (transform.right * sideward * Time.deltaTime) + (transform.up * upward * Time.deltaTime);


        //camera movement
        Vector3 camera = transform.position - transform.forward * 20.0f + Vector3.up * 5.0f;
        Camera.main.transform.position = camera;
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);     
        

        //some borders
        //x axis
        if(transform.position.x < 0.0f)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
        if(transform.position.x > 1000.0f)
        {
            transform.position = new Vector3(1000f, transform.position.y, transform.position.z);
        }
        //z axis
        if(transform.position.z < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }
        if(transform.position.z > 1000.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1000.0f);
        }

        //position of the terrain
        float now = Terrain.activeTerrain.SampleHeight(transform.position);
        //border for the terrain
        if(now > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, now, transform.position.z);
        }

        float border = Terrain.activeTerrain.GetPosition().x;
        if (border > 1000)
        {
            transform.position = new Vector3(border, transform.position.y, transform.position.z);
        }
    }
}
