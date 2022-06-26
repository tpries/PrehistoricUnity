using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private Vector3 movementDirection;
    private float baseSpeed;

    public void SetUp(Vector3 dir, float base_speed)
    {
        this.baseSpeed = base_speed;
        this.movementDirection = dir;
        FindObjectOfType<AudioManager>().PlayFireball();
        StartCoroutine(SelfDestruct());
        
    }

    private void Update()
    {
        float moveSpeed = this.baseSpeed + 500f;
        transform.position += movementDirection * moveSpeed * Time.deltaTime;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
