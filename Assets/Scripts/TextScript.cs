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

    public LevelSystem level_sys;

    //counts the level
    private int counter = 1;
    //counts the score and fails
    private int score, fails;
    //For the collisionline
    public Image collisions;

    [SerializeField]
    private Text waveText;

    // Start is called before the first frame update
    void Start()
    {
        leveltext.text = "Level: " + counter.ToString();
        gametext.text = "";
        finalscore.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        //counting the level
        counter = level_sys.getLevel();
        //and printing it on the canvas
        leveltext.text = "Level: " + counter.ToString();

        UpdateCollisions();
    }

    // BEAUTIFY
    public void UpdateCollisions()
    {
        float duration = 0.75f * (level_sys.life / level_sys.maxLifes);
        collisions.DOFillAmount(level_sys.life / level_sys.maxLifes, duration);
        Color newColor = Color.green;
        if (level_sys.life < level_sys.maxLifes * 0.25f)
        {
            newColor = Color.red;
        }
        else if (level_sys.life < level_sys.maxLifes * 0.66f)
        {
            newColor = new Color(1f, .64f, 0f, 1f);
        }
        collisions.DOColor(newColor, duration);
    }

    public void SetScore(int score)
    {
        score_text.text = "SCORE: " + score.ToString();
    }

    public void SetFails(int fails)
    {
        //after a certain amount of fails, the game will stop
        if (fails > level_sys.maxLifes)
        {
            Debug.Log("what up");
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void DisplayWaveNumber(int waveNumber)
    {
        waveText.enabled = false;
        waveText.text = "Wave " + waveNumber.ToString();

        StartCoroutine(FadeText(waveText, 1f));
    }

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
