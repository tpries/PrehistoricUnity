using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//THIS IS THE SCRIPT FOR THE ASTEROIDS AND LEVELS
public class LevelSystem : MonoBehaviour
{
    //to print the score and collisions, we have to hand them to the TextScript
    private TextScript texts;

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

    private float level_time;

    private float current_level_time, level_length;

    private GameObject[] spawned_asteroids;

    // Update is called once per frame
    void Start()
    {   //instantiate
        score = this.score;
        fails = this.fails;

        texts = GameObject.FindGameObjectWithTag("Text").GetComponent<TextScript>();
        count = 0;
        level = 1;
        life = 20;
        maxLifes = 20;

        Debug.Log("start");

        current_level_time = 0;

        AsteroidShower();
    }


    void Update()
    {

        level_length = 20 + level * 2;
        
        current_level_time += Time.deltaTime;

        if (current_level_time >= level_length && AllAsteroidsDestroyed())
        {   
            current_level_time = 0;
            Debug.Log("Level up! " + level);
            AsteroidShower();
            level++;
        }
    }

    void AsteroidShower()
    {
        // the showers should come in waves
        // 1 Asteroid
        // 2 Asteroids
        // 2 Asteroids a bit further appart
        // 3 Asteroids 
        // 3 Asteroids a bit further appart ...

        // get new seed position for asteroids
        Vector3 shower_position_seed = new Vector3(Random.Range(2000, 4000), Random.Range(5000, 6000), Random.Range(2000, 8000));

        // get prefab to instantiate
        GameObject asteroid = (GameObject)Resources.Load("prefabs/Asteroid", typeof(GameObject));

        // collect spawned asteroids
        spawned_asteroids = new GameObject[(level+1) / 2];

        // create asteroids of asteroid shower
        Debug.Log("# asteroids " + (level+1) / 2);
        for (int i = 0; i < (level+1) / 2; i++)
        {
            // instantiate new asteroid (spawn second asteroid higher)
            GameObject newAsteroid = Instantiate(asteroid, shower_position_seed + Random.insideUnitSphere * level * 200 + new Vector3(0,i * 500, 0), Quaternion.identity) as GameObject;

            // set asteroid scale and mass for speed
            //scale = Random.Range(20, 50);
            //newAsteroid.transform.localScale = new Vector3(scale, scale, scale);
            float mass = 5;// scale;
            float side_wards_movement = Random.Range(-(mass * mass), (mass * mass));
            newAsteroid.GetComponent<Rigidbody>().AddForce(Physics.gravity * (mass * mass) / 1.2f + new Vector3(side_wards_movement, 0, side_wards_movement));

            // collect spawned asteroid
            spawned_asteroids[i] = newAsteroid;
            // wait a bit by starting coroutine as update does not allow us to wait here
            //StartCoroutine(WaitingCoroutine(level*10));
        }
    }


        /*
        //between Levels, there will be a certain amount of time
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

                transform.position = new Vector3(Random.Range(1000, 3000), Random.Range(5000, 6000), Random.Range(4000, 5000));

                // get prefab to instantiate
                GameObject asteroid = (GameObject)Resources.Load("prefabs/Asteroid", typeof(GameObject));

                // execute block of code here
                GameObject newAsteroid = Instantiate(asteroid, transform.position, Quaternion.identity) as GameObject;  // instatiate the object
                scale = Random.Range(20, 100);
                newAsteroid.transform.localScale = new Vector3(scale, scale, scale); // change its local scale in x y z format
                float mass = scale*1.5f;
                float side_wards_movement = Random.Range(-(mass * mass), (mass * mass));
                newAsteroid.GetComponent<Rigidbody>().AddForce(Physics.gravity * (mass * mass) / 1.2f + new Vector3(side_wards_movement, 0, side_wards_movement));

            }

            if (count >= 5)
            {
                count = 0;
                interpolationPeriod = interpolationPeriod - (interpolationPeriod / 2);
                level++;
                cooldown = Time.time + buffer;
            }
        }
    }
    */

    private bool AllAsteroidsDestroyed()
    {
        if (spawned_asteroids == null) return true;

        for (int i = 0; i > spawned_asteroids.Length; i++)
        {
            Debug.Log(spawned_asteroids[i]);
            if (spawned_asteroids[i] != null)
            {
                return false;
            }
        }

        return true;
    }

    public int getLevel()
    {
        return level;
    }

    public void IncreaseScore()
    {
        this.score += 1;
        texts.SetScore(this.score);
    }

    public void IncreaseFails()
    {
        this.fails += 1;
        life--;
        texts.UpdateCollisions();
        texts.SetFails(this.fails);
    }

    public int getScore()
    {
        return score;
    }

    public int getFails()
    {
        return fails;
    }
}
