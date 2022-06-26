using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneHandler : MonoBehaviour
{
    // Start, Game, End
    public string state;

    public string mainScene;

    public Text blinkingText;

    // values for the blinking text
    public float alphaStep;
    private float alpha;
    public Color color;
    private float alphaThresh;


    void Start()
    {
        color = blinkingText.color;
        alpha = 0;
        alphaThresh = 300;
    }

    void Update()
    {

        // compute new alpha value
        // this block of code will always first increase the alpha value to its max (1)
        // and then decrease it to its min (0)
        alpha += alphaStep;

        if (alpha == alphaThresh)
        {
            alpha = 0;
        }

        color.a = alpha / alphaThresh;

        blinkingText.color = color;


        // start game on space down
        if (Input.GetKeyDown("space") && state.Equals("Start"))
        {
            LoadScene();
        }

        // restart game if we are in the gameover screen
        if (Input.GetKeyDown(KeyCode.R) && state.Equals("GameOver"))
        {
            LoadScene();
        }

        // quit application
        if (Input.GetKeyDown(KeyCode.Q) && state.Equals("GameOver"))
        {
            Application.Quit();
        }

    }

    public void LoadScene()
    {
        SceneManager.LoadScene(mainScene);
    }
}
