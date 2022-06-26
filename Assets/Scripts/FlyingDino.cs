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
    public float speedBase;
    private float straight, straightAccelerated, straightUpwards;


    //speed
    private float forward, upward, sideward;

    //acceleration
    private float forwardAcc = 100.0f;

    //mouse control, rotation
    public float rotateSpeed = 80.0f;
    private Vector2 lookInput, center, mouseDistance;

    private float roll;
    public float rollSpeed = 150f, rollAcc = 130.0f;

    private Rigidbody rigidBody;
    private Animator animator;

    public float DISPLAY_FLOAT;

    private float cameraHeightOffset = 5f;
    private float cameraDistanceOffset = 20f;

    public LevelSystem levelSys;

    // Start is called before the first frame update
    void Start()
    {

        // set straight acc and upwards relative to straight
        straight = speedBase;
        straightAccelerated = speedBase + 400;
        straightUpwards = speedBase - 400;

        // get animator
        animator = GetComponent<Animator>();

        // get rigidbody and set values
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.drag = 0.75f;
        rigidBody.mass = 0.5f;
        rigidBody.angularDrag = 10f;

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
        mouseDistance.x = (lookInput.x - center.x) / center.x;
        mouseDistance.y = (lookInput.y - center.y) / center.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 5.5f);

        //rolling through space
        roll = Mathf.Lerp(roll, Input.GetAxisRaw("Roll"), rollAcc * Time.deltaTime);

        //rotation
        transform.Rotate(-mouseDistance.y * rotateSpeed * Time.deltaTime, mouseDistance.x * rotateSpeed * Time.deltaTime, roll * rollSpeed * Time.deltaTime, Space.Self);

        // set animation for active flight
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetBool("active_flight", true);
            FindObjectOfType<AudioManager>().Play("Flapping");

        }
        else
        {
            animator.SetBool("active_flight", false);

            if (this.rigidBody.velocity.magnitude > 0 && Input.GetAxisRaw("Vertical") < 0)
            {
                this.rigidBody.velocity = new Vector3(0, 0, 0);
            }

            if (this.rigidBody.velocity.magnitude < 30)
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
        forward = Mathf.Lerp(forward, Input.GetAxisRaw("Vertical") * straight, forwardAcc * Time.deltaTime);
        //sideward = Mathf.Lerp(sideward, Input.GetAxisRaw("Horizontal") * side, side_acc * Time.deltaTime);
        //upward = Mathf.Lerp(upward, Input.GetAxisRaw("Hover") * ascend, ascend_acc * Time.deltaTime);

        // change speed depending on look direction
        if (mouseDistance.y > 1)
        {
            straight = straightUpwards;
        }
        else if (mouseDistance.y < -1)
        {
            straight = straightAccelerated;
        }
        else
        {
            straight = speedBase;
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
        if (this.rigidBody.velocity.magnitude < 50f)
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
            this.rigidBody.AddForce(new Vector3(0f, -10f, 0f));
        }

        Vector3 direction = transform.forward * forward * 30 * Time.deltaTime + transform.right * sideward * Time.deltaTime + transform.up * upward * Time.deltaTime;

        //the actual movement
        this.rigidBody.AddForce(direction);
    }

    void CameraFollows()
    {
        //camera movement
        camera = transform.position - transform.forward * cameraDistanceOffset + Vector3.up * this.cameraHeightOffset;
        Camera.main.transform.position = camera;
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);
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
            levelSys.IncreaseScore();
        }
    }

}

