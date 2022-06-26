using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS THE HEART OF IT ALL, OUR FLYING DINOSAUR
public class FlyingDino : MonoBehaviour
{
    [SerializeField]
    private Transform fireballPrefab;

    // speed variables
    public float speedBase;
    private float speed, straightAccelerated, straightUpwards, forward;

    //mouse control, rotation
    public float rotateSpeed = 80.0f;

    // center of the screen
    private Vector2 center;

    // rotation speed values
    private float roll;
    public float rollSpeed = 80f, rollAcc = 130.0f;

    private float cameraHeightOffset = 5f;
    private float cameraDistanceOffset = 20f;

    // Rigidbody, Animator and LevelSystem
    private Rigidbody rigidBody;
    private Animator animator;

    public LevelSystem levelSys;

    void Start()
    {

        // set speed acc and upwards relative to speedBase
        speed = speedBase;
        straightAccelerated = speedBase + 400;
        straightUpwards = speedBase - 400;

        // get animator
        animator = GetComponent<Animator>();

        // get rigidbody and set values
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.drag = 0.75f;
        rigidBody.mass = 0.5f;
        rigidBody.angularDrag = 10f;

        // set position
        transform.position = new Vector3(3900f, 2300f, 700f);

        //instantiate the center of the screen
        center.x = Screen.width * 0.5f;
        center.y = Screen.height * 0.5f;
    }

    // In every update:
    // Move dino
    // adjust camera
    // shoot fireball if wanted
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
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        //check for the mouse distance from the center
        Vector2 mouseDistance;
        mouseDistance.x = (mouseX - center.x) / center.x;
        mouseDistance.y = (mouseY - center.y) / center.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 5.5f);

        // roll dino left and right with "a" and "d" and mouse position
        roll = Mathf.Lerp(roll, Input.GetAxisRaw("Roll"), rollAcc * Time.deltaTime);
        transform.Rotate(-mouseDistance.y * rotateSpeed * Time.deltaTime, mouseDistance.x * rotateSpeed * Time.deltaTime, roll * rollSpeed * Time.deltaTime, Space.Self);

        // set animation for active flight
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetBool("active_flight", true);
            FindObjectOfType<AudioManager>().Play("Flapping");

            // change speed depending on look direction
            if (mouseDistance.y > 1) // sturzflug
            {
                speed = straightAccelerated;
                animator.SetBool("sturz", true);
                FindObjectOfType<AudioManager>().Stop("Flapping");
            }
            else if (mouseDistance.y < -1) // upwards is harder
            {
                speed = straightUpwards;
                animator.SetBool("sturz", false);

            }
            else
            {
                speed = speedBase;
                animator.SetBool("sturz", false);

            }

        }
        else
        {
            speed = 0;

            animator.SetBool("active_flight", false);

            // break if "s" is pressed
            if (this.rigidBody.velocity.magnitude > 0 && Input.GetAxisRaw("Vertical") < 0)
            {
                this.rigidBody.velocity = new Vector3(0, 0, 0);
            }

            // idle air animation is played when dino is slow
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

        // set animation
        if (this.rigidBody.velocity.magnitude < 50f)
        {
            animator.SetBool("idle_air", true);

            // decrease volume of wind during flight if dino is slow
            FindObjectOfType<AudioManager>().DecreaseVolume("Flight");
        }
        else
        {
            animator.SetBool("idle_air", false);
            
            // increase volume of wind during flight if dino is faster
            FindObjectOfType<AudioManager>().IncreaseVolume("Flight");
        }

        // check for shift-L 
        // if key is pressed dino will lower y position
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.rigidBody.AddForce(new Vector3(0f, -10f, 0f));
        }

        // get movement direction (and force)
        Vector3 direction = transform.forward * speed * 30 * Time.deltaTime;

        //the actual movement
        this.rigidBody.AddForce(direction);
    }

    // Here we just always update the camera position according to the positon of the dinosaur
    void CameraFollows()
    {
        Camera.main.transform.position = transform.position - transform.forward * cameraDistanceOffset + Vector3.up * cameraHeightOffset;
        Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);
    }

    // shoot fireball
    public void ShootFireBall()
    {
        // instantiate prefab
        Transform fireballTransform = Instantiate(fireballPrefab, transform.position + transform.forward*3 + new Vector3(0, 4, 0), Quaternion.identity);

        // get shooting direction
        Vector3 shootDir = Vector3.Normalize(transform.forward);

        // setup fireball
        fireballTransform.GetComponent<FireballScript>().SetUp(shootDir, speed*1.5f);
    }

    // if dino collides with asteroid, increase score in LevelSystem
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().tag == "Asteroid")
        {
            levelSys.IncreaseScore();
        }
    }

}

