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
    public Text levelText;
    //for "game over"
    public Text endText;
    //the score during the game
    public Text currentScore;
    //the fial score
    public Text finalScore;

    public LevelSystem levelSys;

    //counts the level
    private int counter = 1;
    //counts the score and fails
    private int score, fails;
    //For the lifebar
    public Image lifes;

    [SerializeField]
    private Text waveText;

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level: " + counter.ToString();
        endText.text = "";
        finalScore.text = "";

        currentScore.text = "SCORE: 0";

    }

    // Update is called once per frame
    void Update()
    {

        //counting the level
        counter = levelSys.getLevel();
        //and printing it on the canvas
        levelText.text = "Level: " + counter.ToString();

        //For the lifebar
        UpdateCollisions();
    }

    // BEAUTIFY THE LIFEBAR 
    // gradually reduce the filling of the bar when damage of the terrain gets greater
    // after ca. a third, the green bar gets orange
    // and in the end red
    public void UpdateCollisions()
    {
        float duration = 0.75f * (levelSys.life / levelSys.maxLifes);
        lifes.DOFillAmount(levelSys.life / levelSys.maxLifes, duration);
        Color newColor = Color.green;
        if (levelSys.life < levelSys.maxLifes * 0.25f)
        {
            newColor = Color.red;
        }
        else if (levelSys.life < levelSys.maxLifes * 0.66f)
        {
            //this is cool code for "orange" (RGB)
            newColor = new Color(1f, .64f, 0f, 1f);
        }
        lifes.DOColor(newColor, duration);
    }

    public void SetScore(int score)
    {
        // we encountered the problem that apparently more than one LevelSystem instance existed at runtime
        // the other instance would then always set the value to 0
        // this is our quick and hacky fix
        if (score == 0) return;


        currentScore.text = "SCORE: " + score.ToString();
    }

    public void SetFails(int fails)
    {

        // we encountered the problem that apparently more than one LevelSystem instance existed at runtime
        // the other instance would then always set the value to 0
        // this is our quick and hacky fix
        if (fails == 0) return;

        //after a certain amount of fails, the game will stop
        if (fails > levelSys.maxLifes)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    // Wave number is a text that should fade in and out
    // start coroutine therefore
    public void DisplayWaveNumber(int waveNumber)
    {
        waveText.enabled = false;
        waveText.text = "Wave " + waveNumber.ToString();

        StartCoroutine(FadeText(waveText, 1f));
    }

    // first fade text in, then out
    // enable text at beginning and disable at end
    public IEnumerator FadeText(Text text, float t)
    {

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.enabled = true;
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / t));
            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));

            yield return null;
        }

        text.enabled = false;

    }
}
