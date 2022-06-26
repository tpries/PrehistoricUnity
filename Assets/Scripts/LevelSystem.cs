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
    //level for the level
    private int score, fails, level;


    //for the lifebar
    public float life, maxLifes;

    //for time and level management
    private float currentLevelTime, levelLength;

    //for asteroid control
    private GameObject[] spawnedAsteroids;

    // Update is called once per frame
    void Start()
    {   //instantiate
        score = this.score;
        fails = this.fails;

        texts = GameObject.FindGameObjectWithTag("Text").GetComponent<TextScript>();
        level = 0;
        life = 20;
        maxLifes = 20;

        Debug.Log("start");

        currentLevelTime = 0;
    }


    void Update()
    {
        //TOM JETZT DU
        levelLength = 20 + level * 2;
        
        currentLevelTime += Time.deltaTime;

        if (currentLevelTime >= levelLength && AllAsteroidsDestroyed())
        {   
            currentLevelTime = 0;
            level++;
            texts.DisplayWaveNumber(level);
            AsteroidShower();
        }
    }

    void AsteroidShower()
    {
        // the showers should come in waves of increasing difficulty
        // 1 Asteroid
        // 2 Asteroids
        // 2 Asteroids a bit further apart
        // 3 Asteroids 
        // 3 Asteroids a bit further apart ...

        // get new seed position for asteroids
        Vector3 showerPositionSeed = new Vector3(Random.Range(2000, 4000), Random.Range(5000, 6000), Random.Range(2000, 8000));

        // get prefab to instantiate
        GameObject asteroid = (GameObject)Resources.Load("prefabs/Asteroid", typeof(GameObject));

        // collect spawned asteroids
        spawnedAsteroids = new GameObject[(level+1) / 2];

        // create asteroids of asteroid shower
        Debug.Log("# asteroids " + (level+1) / 2);
        for (int i = 0; i < (level+1) / 2; i++)
        {
            // instantiate new asteroid (spawn second asteroid higher)
            GameObject newAsteroid = Instantiate(asteroid, showerPositionSeed + Random.insideUnitSphere * level * 200 + new Vector3(0,i * 500, 0), Quaternion.identity) as GameObject;

            // set asteroid mass for speed
            float mass = 2;
            float sidewardsMovement = Random.Range(-(mass * mass), (mass * mass));
            newAsteroid.GetComponent<Rigidbody>().AddForce(Physics.gravity * (mass * mass) / 1.2f + new Vector3(sidewardsMovement, 0, sidewardsMovement));

            // collect spawned asteroid
            spawnedAsteroids[i] = newAsteroid;
        }
    }

    //?
    private bool AllAsteroidsDestroyed()
    {
        if (spawnedAsteroids == null) return true;

        for (int i = 0; i > spawnedAsteroids.Length; i++)
        {
            Debug.Log(spawnedAsteroids[i]);
            if (spawnedAsteroids[i] != null)
            {
                return false;
            }
        }

        return true;
    }

    //Getter for the level
    public int getLevel()
    {
        return level;
    }
    //Getter for the score
    public int getScore()
    {
        return score;
    }
    //Getter for the fails
    public int getFails()
    {
        return fails;
    }

    //increase the score and hand it to the TextScript
    public void IncreaseScore()
    {
        this.score += 1;
        texts.SetScore(this.score);
    }
    //increase the fails and hand it to the TextScript
    public void IncreaseFails()
    {
        this.fails += 1;
        life--;
        texts.UpdateCollisions();
        texts.SetFails(this.fails);
    }

}
