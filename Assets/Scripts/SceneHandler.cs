using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneHandler : MonoBehaviour
{

    public string state;

    public string main_scene;

    public Text blinking_text;

    public float alpha_step;

    private float alpha;

    public Color color;

    private float alpha_thresh;


    // Start is called before the first frame update
    void Start()
    {
        color = blinking_text.color;
        alpha = 0;
        alpha_thresh = 300;
    }

    // Update is called once per frame
    void Update()
    {
        alpha += alpha_step;

        if (alpha == alpha_thresh)
        {
            alpha = 0;
        }

        color.a = alpha / alpha_thresh;

        blinking_text.color = color;

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
        SceneManager.LoadScene(main_scene);
    }
}
