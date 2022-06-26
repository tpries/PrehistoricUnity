using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneHandler : MonoBehaviour
{

    public string state;

    public string mainScene;

    public Text blinkingText;

    public float alphaStep;

    private float alpha;

    public Color color;

    private float alphaThresh;


    // Start is called before the first frame update
    void Start()
    {
        color = blinkingText.color;
        alpha = 0;
        alphaThresh = 300;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.R) && state.Equals("GameOver"))
        {
            LoadScene();
        }

        if (Input.GetKeyDown(KeyCode.Q) && state.Equals("GameOver"))
        {
            // quit
        }

    }

    public void LoadScene()
    {
        SceneManager.LoadScene(mainScene);
    }
}
