using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDino : MonoBehaviour
{
    //speed
    public float straight = 100.0f, side = 0.0f, ascend = 0.0f;
    private float forward, upward, sideward;

    //acceleration
    private float forward_acc = 20.0f;

    //mouse control, rotation
    public float rotatespeed = 50.0f;
    private Vector2 lookInput, center, mouse_distance;

    private float roll;
    public float roll_speed = 50f, roll_acc = 30.0f;
    
    private Rigidbody rigid_body;
    private Animator animator;

    public float DISPLAY_FLOAT;
    
    // Start is called before the first frame update
    void Start() {

        animator = GetComponent<Animator>();

        rigid_body = GetComponent<Rigidbody>();
        rigid_body.drag = 0.75f;
        rigid_body.mass = 0.5f;
        rigid_body.angularDrag = 10f;

        transform.position = new Vector3(0f, 400f, 0f);
        
        //instantiate the center of the screen
        center.x = Screen.width * 0.5f;
        center.y = Screen.height * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        DinoMovement();

        CameraFollows();

        //Borders();
    
    }

    void DinoMovement()
    {
        //check where the mouse is
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        //check for the distance from the center
        mouse_distance.x = (lookInput.x - center.x) / center.x;
        mouse_distance.y = (lookInput.y - center.y) / center.y;
        mouse_distance = Vector2.ClampMagnitude(mouse_distance, 1.5f);

        //rolling through space
        roll = Mathf.Lerp(roll, Input.GetAxisRaw("Roll"), roll_acc * Time.deltaTime);

        //rotation
        transform.Rotate(-mouse_distance.y * rotatespeed * Time.deltaTime, mouse_distance.x * rotatespeed * Time.deltaTime, roll * roll_speed * Time.deltaTime, Space.Self);

        // set animation for active flight
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetBool("active_flight", true);
        }
        else
        {
            animator.SetBool("active_flight", false);

            if(this.rigid_body.velocity.magnitude > 0 && Input.GetAxisRaw("Vertical") < 0)
            {
                this.rigid_body.velocity = new Vector3(0, 0, 0);
            }
        }

        //speed instantiation
        forward = Mathf.Lerp(forward, Input.GetAxisRaw("Vertical") * straight, forward_acc * Time.deltaTime);
        //sideward = Mathf.Lerp(sideward, Input.GetAxisRaw("Horizontal") * side, side_acc * Time.deltaTime);
        //upward = Mathf.Lerp(upward, Input.GetAxisRaw("Hover") * ascend, ascend_acc * Time.deltaTime);

        // change speed depending on look direction
        if (mouse_distance.y > 1)
        {
            straight = 40f;
        }
        else if (mouse_distance.y < -1)
        {
            straight = 150f;
        }
        else
        {
            straight = 100f;
        }

        // geht straight runter mit der Zeit?
        //speed up when going down/ slower when ascending
        /*straight -= transform.forward.y * Time.deltaTime * 20.0f;
        //minimal speed
        if (straight < 25.0f)
        {
            straight = 25.0f;
        }
        */

        if (transform.forward.y < -0.85)
        {
            animator.SetBool("sturz", true);
        }
        else
        {
            animator.SetBool("sturz", false);
        }

        this.DISPLAY_FLOAT = Input.GetAxisRaw("Vertical") * straight;

        // set animation
        if (this.rigid_body.velocity.magnitude < 30f)
        {
            animator.SetBool("idle_air",true);
        }
        else
        {
            animator.SetBool("idle_air", false);
        }

        // check for shift-L 
        // if key is pressed dino will lower y position
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.rigid_body.AddForce(new Vector3(0f, -10f, 0f));
        }

        Vector3 direction = transform.forward * forward * 30 * Time.deltaTime + transform.right * sideward * Time.deltaTime + transform.up * upward * Time.deltaTime;

        //the actual movement
        this.rigid_body.AddForce(direction);
    }

    void CameraFollows()
    {
        //camera movement
        Vector3 camera = transform.position - transform.forward * 20.0f + Vector3.up * 5.0f;
        Camera.main.transform.position = camera;
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);
    }

    void Borders()
    {
        //some borders
        //x axis
        if (transform.position.x < 0.0f)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 1000.0f)
        {
            transform.position = new Vector3(1000f, transform.position.y, transform.position.z);
        }
        //z axis
        if (transform.position.z < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }
        if (transform.position.z > 1000.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1000.0f);
        }

        /*
        //position of the terrain
        float now = Terrain.GetHeight(transform.position);
        //border for the terrain
        if (now > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, now, transform.position.z);
        }
        */
    }
}

