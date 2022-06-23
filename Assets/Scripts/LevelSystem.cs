using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//THIS IS THE SCRIPT FOR THE ASTEROIDS AND LEVELS
public class LevelSystem : MonoBehaviour
{
    //to print the score and collisions, we have to hand them to the TextScript
    public TextScript texts;
    
    //the asteroid is to be catched 
    public GameObject asteroid;
    //score: how many asteroids are catched
    //fails: how many collisions there are between asteroids and earth
    private int score, fails, count, level;
    //for time management
    private float time = 0.0f, timestamp = 0.0f, cooldown = 3.0f, buffer = 5.0f;
    //in which frequence asteroids are to be spawned
    private float interpolationPeriod = 10f;

    private float scale;

    //for the collisionbar
    public float life, maxLifes;



    // Update is called once per frame
    void Start()
    {   //instantiate
        score = this.score;
        fails = this.fails;
        texts.SetScore();
        texts.SetFails();
        count = 0;
        level = 0;
        life = 20;
        maxLifes = 20;
    }


    void Update()
    {
        AsteroidShower();
 
    }

    void AsteroidShower()
    {
        //between Levels, there will be a certain amount of tim
        if (cooldown < Time.time)
        {

            timestamp += Time.deltaTime;

            //to count the amount of calls
            if (timestamp >= 5)
            {
                count++;
                timestamp = 0.0f;
            }


            time += Time.deltaTime;

            if (time >= interpolationPeriod)
            {
                time = time - interpolationPeriod;

                transform.position = new Vector3(Random.Range(1000, 3000), Random.Range(2000, 3000), Random.Range(4000, 5000));

                // execute block of code here
                GameObject newAsteroid = Instantiate(asteroid, transform.position, Quaternion.identity) as GameObject;  // instatiate the object
                scale = Random.Range(20, 200);
                newAsteroid.transform.localScale = new Vector3(scale, scale, scale); // change its local scale in x y z format
                float mass = 40 + scale;
                float side_wards_movement = Random.Range(-(mass*mass)*10, (mass * mass)*10);
                newAsteroid.GetComponent<Rigidbody>().AddForce(Physics.gravity * (mass * mass) + new Vector3(side_wards_movement, 0, side_wards_movement));

            }

            if (count >= 2)
            {
                count = 0;
                interpolationPeriod = interpolationPeriod - (interpolationPeriod / 2);
                level++;
                cooldown = Time.time + buffer;
            }
        }
    }


    public int getLevel()
    {
        return level;
    }
    public void IncreaseScore()
    {
        this.score += 1;
        texts.SetScore();
    }

    public void IncreaseFails()
    {
        this.fails += 1;
        life--;
        texts.UpdateCollisions();
        texts.SetFails();
    }

    public int getScore()
    {
        return score;
    }

    public int getFails()
    {
        return fails;
    }


    /*
    void Update()
    {

        //Level 1
        //while ( 3 < (Time.time - startTime) && (Time.time - startTime) < 7)
        if ((Time.time - startTime) % 10 == 0)
        {
            Instantiate(goal, transform.position, Quaternion.identity);
            Debug.Log("hereh1");
            Debug.Log("hereh1");
            Debug.Log("hereh1");
            Debug.Log("hereh1");
            Debug.Log("hereh1");
            Debug.Log("hereh1");
        }
    */

    /*//rotation of the object
    goal.transform.Rotate(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);

    //destroy true after 20 seconds
    if (Time.time - startTime > 20)
    {
        destroy = true;
    }

    //destruction: just move the object to a different random position and thereby "destroy" it
    if(destroy == true)
    {
        goal.transform.position = new Vector3(Random.Range(1, 999), Random.Range(250, 350), Random.Range(1, 999));
        destroy = false;
        //startTime = Time.time;
    }*/

    /*if (bounce == true)
    {
        transform.position = new Vector3(transform.position.x, curve.Evaluate((Time.time % curve.length)), transform.position.z);
    }
    else
    {
        transform.Translate(0, -40, 0 * Time.deltaTime);
    }*/


    /*private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collider>().tag == "Dino")
        {
            Destroy(this.gameObject);
            //destroy = true;
            score++;
            //this.goal.transform.position = new Vector3(Random.Range(1, 999), Random.Range(250, 350), Random.Range(1, 999));
            //dino.Grow();
        }

        if(other.GetComponent<Collider>().tag == "Terrain")
        {
            Destroy(this.gameObject);
            fails++;
        }
        //other.transform.localScale += new Vector3(1f, 1f, 1f);
    }

    public int getScore()
    {
        return score;
    }

    public int getFails()
    {
        return fails;
    }*/
}



       /* x = Random.Range(1f, 999f);
        y = Random.Range(250f, 350f);
        z = Random.Range(1f, 999f);

        transform.position = new Vector3(Random.Range(1f, 999f), Random.Range(250f, 350f), Random.Range(1f, 999f));

        Console.log (transform.position.y) ;

        need = obj.transform.position.y;

        if (obj.transform.position.y == need || obj.transform.position.y < (need+10f))
        {
            obj.transform.position = new Vector3(0, 10f, 0) * Time.deltaTime * spee;
        }
        if (transform.position.y == (need + 10))
        {
            obj.transform.position = new Vector3(0, -10f, 0) * Time.deltaTime * spee;
        }
    }*/

