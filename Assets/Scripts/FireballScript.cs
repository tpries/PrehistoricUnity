using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Vector3 movementDirection;
    private float base_speed;

    public void SetUp(Vector3 dir, float base_speed)
    {
        this.base_speed = base_speed;
        this.movementDirection = dir;
    }

    private void Update()
    {
        float moveSpeed = this.base_speed + 200f;
        Debug.Log(moveSpeed + " - " + this.base_speed);
        transform.position += movementDirection * moveSpeed * Time.deltaTime;
    }
    
}
