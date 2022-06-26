using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// can be added to game object to display sound
public class SingleSoundDisplay : MonoBehaviour
{
    // intervall at which the sound should be played
    [SerializeField]
    private float intervall;

    // name of the sound that should be played
    [SerializeField]
    private string sound_name;

    // should it be a fixed intervall or a bit random?
    public bool random = false;

    void Start()
    {   
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        // loop sound
        while (true)
        {
            float random_time = 0;

            if (random)
            {
                random_time = Random.Range(-3f, 3.0f);
            }

            // wait for intervall time

            yield return new WaitForSeconds(intervall + random_time);
            FindObjectOfType<AudioManager>().Play(sound_name);
        }
    }
}
