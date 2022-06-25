using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS THE HEART OF IT ALL, OUR FLYING DINOSAUR
public class FlyingDino : MonoBehaviour
{
    [SerializeField]
    private Transform fireballPrefab;

    //camera
    public Vector3 camera;

    // speeds
    public float speed_base;
    private float straight, straight_accelerated, straight_upwards;


    //speed
    private float forward, upward, sideward;

    //acceleration
    private float forward_acc = 100.0f;

    //mouse control, rotation
    public float rotatespeed = 80.0f;
    private Vector2 lookInput, center, mouse_distance;

    private float roll;
    public float roll_speed = 150f, roll_acc = 130.0f;

    private Rigidbody rigid_body;
    private Animator animator;

    public float DISPLAY_FLOAT;

    private float camera_height_offset = 5f;
    private float camera_distance_offset = 20f;

    public LevelSystem level_sys;

    // Start is called before the first frame update
    void Start()
    {

        // set straight acc and upwards relative to straight
        straight = speed_base;
        straight_accelerated = speed_base + 400;
        straight_upwards = speed_base - 400;

        // get animator
        animator = GetComponent<Animator>();

        // get rigidbody and set values
        rigid_body = GetComponent<Rigidbody>();
        rigid_body.drag = 0.75f;
        rigid_body.mass = 0.5f;
        rigid_body.angularDrag = 10f;

        transform.position = new Vector3(3900f, 2300f, 700f);

        //instantiate the center of the screen
        center.x = Screen.width * 0.5f;
        center.y = Screen.height * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        DinoMovement();

        CameraFollows();

        // shoot fireball on space down
        if (Input.GetKeyDown("space"))
        {
            ShootFireBall();
        }
    }

    void DinoMovement()
    {
        // play sounds
        FindObjectOfType<AudioManager>().Play("Flight");

        //check where the mouse is
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        //check for the distance from the center
        mouse_distance.x = (lookInput.x - center.x) / center.x;
        mouse_distance.y = (lookInput.y - center.y) / center.y;
        mouse_distance = Vector2.ClampMagnitude(mouse_distance, 5.5f);

        //rolling through space
        roll = Mathf.Lerp(roll, Input.GetAxisRaw("Roll"), roll_acc * Time.deltaTime);

        //rotation
        transform.Rotate(-mouse_distance.y * rotatespeed * Time.deltaTime, mouse_distance.x * rotatespeed * Time.deltaTime, roll * roll_speed * Time.deltaTime, Space.Self);

        // set animation for active flight
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetBool("active_flight", true);
            FindObjectOfType<AudioManager>().Play("Flapping");

        }
        else
        {
            animator.SetBool("active_flight", false);

            if (this.rigid_body.velocity.magnitude > 0 && Input.GetAxisRaw("Vertical") < 0)
            {
                this.rigid_body.velocity = new Vector3(0, 0, 0);
            }

            if (this.rigid_body.velocity.magnitude < 30)
            {
                FindObjectOfType<AudioManager>().Play("Flapping");
            }
            else
            {
                // stop flapping wings if no active flight or idle
                FindObjectOfType<AudioManager>().Stop("Flapping");
            }
        }

        //speed instantiation
        forward = Mathf.Lerp(forward, Input.GetAxisRaw("Vertical") * straight, forward_acc * Time.deltaTime);
        //sideward = Mathf.Lerp(sideward, Input.GetAxisRaw("Horizontal") * side, side_acc * Time.deltaTime);
        //upward = Mathf.Lerp(upward, Input.GetAxisRaw("Hover") * ascend, ascend_acc * Time.deltaTime);

        // change speed depending on look direction
        if (mouse_distance.y > 1)
        {
            straight = straight_upwards;
        }
        else if (mouse_distance.y < -1)
        {
            straight = straight_accelerated;
        }
        else
        {
            straight = speed_base;
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
            FindObjectOfType<AudioManager>().Stop("Flapping");
        }
        else
        {
            animator.SetBool("sturz", false);
        }

        this.DISPLAY_FLOAT = Input.GetAxisRaw("Vertical") * straight;

        // set animation
        if (this.rigid_body.velocity.magnitude < 50f)
        {
            animator.SetBool("idle_air", true);

            FindObjectOfType<AudioManager>().DecreaseVolume("Flight");
        }
        else
        {
            animator.SetBool("idle_air", false);
            FindObjectOfType<AudioManager>().IncreaseVolume("Flight");
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
        camera = transform.position - transform.forward * camera_distance_offset + Vector3.up * this.camera_height_offset;
        Camera.main.transform.position = camera;
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);
    }

    public void Grow()
    {
        this.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        this.camera_distance_offset += 0.5f;
        this.camera_height_offset += 0.5f;
        this.camera_height_offset += 0.5f;
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
    }

    public void ShootFireBall()
    {
        Transform fireballTransform = Instantiate(fireballPrefab, transform.position + transform.forward*3 + new Vector3(0, 4, 0), Quaternion.identity);

        Vector3 shootDir = Vector3.Normalize(transform.forward);

        fireballTransform.GetComponent<FireballScript>().SetUp(shootDir, this.straight*1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().tag == "Asteroid")
        {
            level_sys.IncreaseScore();
        }
    }

}

