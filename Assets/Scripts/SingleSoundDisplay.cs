using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSoundDisplay : MonoBehaviour
{

    public float intervall;
    public string sound_name;
    public bool random = false;

    void Start()
    {   
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        while (true)
        {
            float random_time = 0;

            if (random)
            {
                random_time = Random.Range(-3f, 3.0f);
            }

            yield return new WaitForSeconds(intervall + random_time);
            FindObjectOfType<AudioManager>().Play(sound_name);
        }
    }
}
