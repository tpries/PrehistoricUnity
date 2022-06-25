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

    // Update is called once per frame
    void Awake()
    {   //instantiate
        score = this.score;
        fails = this.fails;

        texts = GameObject.FindGameObjectWithTag("Text").GetComponent<TextScript>();
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
