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
        FindObjectOfType<AudioManager>().PlayFireball();
        StartCoroutine(SelfDestruct());
        
    }

    private void Update()
    {
        float moveSpeed = this.base_speed + 800f;
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
