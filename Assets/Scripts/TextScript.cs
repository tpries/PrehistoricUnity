using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


//THIS IS THE SCRIPT FOR ALL THE TEXTS IN THE GAME
public class TextScript : MonoBehaviour
{
    //the level
    public Text leveltext;
    //for "game over"
    public Text gametext;
    //the score during the game
    public Text score_text;
    //the fial score
    public Text finalscore;
    //the collisions
    public Text collision_text;

    private LevelSystem level;
    //counts the level
    private int counter = 1;
    //counts the score and fails
    private int score = 0, fails = 0;
    //For the collisionline
    public Image collisions;
  


    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelSystem>();

        leveltext.text = "Level: " + counter.ToString();
        gametext.text = "";
        finalscore.text = "";

        collision_text.text = "COLLISIONS: 0";
        score_text.text = "SCORE: 0";

    }

    // Update is called once per frame
    void Update()
    {
        //counting the level
        counter = level.getLevel();
        //and printing it on the canvas
        leveltext.text = "Level: " + counter.ToString();

        UpdateCollisions();

    
        //after a certain amount of fails, the game will stop
        if(fails > 20)
        {
            //for that, we delete all the enemies
            foreach (Transform child in level.transform)
            {
                Destroy(child.gameObject);
            }
            //and print that the game is over
            gametext.text = "GAME OVER";
            //additionally, the reached score
            finalscore.text = "SCORE: " + score.ToString();

        }
    }

    public void UpdateCollisions()
    {
        collisions.fillAmount = Mathf.Clamp(level.life / level.maxLifes, 0, 1f);
    }

    public void SetScore(int score)
    {

        // we encountered the problem that apparently more than one LevelSystem instance existed at runtime
        // the other instance would then always set the value to 0
        // this is our quick and hacky fix
        if (score == 0) return;
        
        score_text.text = "SCORE: " + score.ToString();
    }

    public void SetFails(int fails)
    {
        // we encountered the problem that apparently more than one LevelSystem instance existed at runtime
        // the other instance would then always set the value to 0
        // this is our quick and hacky fix
        if (fails == 0) return;

        collision_text.text = "COLLISIONS: " + fails.ToString();
    }
}
