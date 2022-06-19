using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circeling : MonoBehaviour
{

    public Transform cadaver;
    public float off_set;

    public float speed;
    private Vector3 on_circle_offset, desired_position;
    private float angle;
    
    private void LateUpdate()
    {
        transform.position = cadaver.position + new Vector3(0f, off_set, 0f);
    // - speed because i cant be arsed to turn the dino around right now
    transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
