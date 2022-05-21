using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDino : MonoBehaviour
{
    //speed
    public float straight = 25.0f, side = 7.5f, ascend = 5.0f;
    private float forward, upward, sideward;

    //acceleration
    private float forward_acc = 2.5f, side_acc = 2.0f, ascend_acc = 2.0f;

    //mouse control, rotation
    public float rotatespeed = 80.0f;
    private Vector2 lookInput, center, mouse_distance;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0f, 0f, 0f);

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

        mouse_distance = Vector2.ClampMagnitude(mouse_distance, 1f);


        //rotation
        transform.Rotate(-mouse_distance.y * rotatespeed * Time.deltaTime, mouse_distance.x * rotatespeed * Time.deltaTime,0f, Space.Self);


        //speed instantiation
        forward = Mathf.Lerp(forward, Input.GetAxisRaw("Vertical") * straight, forward_acc * Time.deltaTime);
        sideward = Mathf.Lerp(sideward, Input.GetAxisRaw("Horizontal") * side, side_acc * Time.deltaTime);
        upward = Mathf.Lerp(upward, Input.GetAxisRaw("Hover") * ascend, ascend_acc * Time.deltaTime);

        transform.position += transform.forward * forward * Time.deltaTime;
        transform.position += (transform.right * sideward * Time.deltaTime) + (transform.up * upward * Time.deltaTime);

        


        //camera movement
        Vector3 camera = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f;
        Camera.main.transform.position = camera;
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);       
        
        //position of the terrain
        float now = Terrain.activeTerrain.SampleHeight(transform.position);
        //border for the terrain
        if(now > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, now, transform.position.z);
        }
    }
}
