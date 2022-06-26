using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Vector3 movementDirection;
    
    private float baseSpeed;

    // set up direction and base speed of fireball
    public void SetUp(Vector3 dir, float base_speed)
    { 
        this.baseSpeed = base_speed;
        this.movementDirection = dir;

        // play fireball sound
        FindObjectOfType<AudioManager>().PlayFireball();

        // destroy after x seconds
        StartCoroutine(SelfDestruct());
        
    }

    private void Update()
    {
        // add some on the basespeed so that the fireball is faster than the flying dino
        float moveSpeed = this.baseSpeed + 500f;
     
        transform.position += movementDirection * moveSpeed * Time.deltaTime;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().tag != "Dino") Destroy(gameObject);
    }
}
