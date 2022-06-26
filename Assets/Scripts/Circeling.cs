using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circeling : MonoBehaviour
{
    // cadaver that the pterodactyls should circle
    public Transform cadaver;

    // height off the circeling dino
    public float off_set;

    // flying speed
    public float speed;

    // cirlce values
    private Vector3 on_circle_offset, desired_position;
    private float angle;
    
    private void LateUpdate()
    {
        // set circleing dino at offset height right above the cadaver
        transform.position = cadaver.position + new Vector3(0f, off_set, 0f);

        // rotate game object (stick with dino, makes it look like its flying
        // - speed because i cant be arsed to turn the dino around right now
        transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
