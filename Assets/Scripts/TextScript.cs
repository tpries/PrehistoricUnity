using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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



    private LevelSystem level;

    //counts the level
    private int counter = 1;
    //counts the score and fails
    private int score, fails;
    //For the collisionline
    public Image collisions;



    // Start is called before the first frame update
    void Start()
    {

        level = GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelSystem>();


        leveltext.text = "Level: " + counter.ToString();
        gametext.text = "";
        finalscore.text = "";

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


    }

    public void UpdateCollisions()
    {
        float life = GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelSystem>().life;
        float maxLifes = GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelSystem>().maxLifes;
        float duration = 0.75f * (level.life / level.maxLifes);
        collisions.DOFillAmount(level.life / level.maxLifes, duration);
        Color newColor = Color.green;
        if (level.life < level.maxLifes * 0.25f)
        {
            newColor = Color.red;
        }
        else if (level.life < level.maxLifes * 0.66f)
        {
            newColor = new Color(1f, .64f, 0f, 1f);
        }
        collisions.DOColor(newColor, duration);
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

        //after a certain amount of fails, the game will stop
        if (fails > level.maxLifes)
        {
            //SceneManager.LoadScene("GameOver");
        }

    }
}
