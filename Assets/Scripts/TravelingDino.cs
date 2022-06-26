using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingDino : MonoBehaviour
{
    public Terrain terrain;

    // targets that walk to
    [SerializeField]
    private Transform[] targets;

    public float speed;

    private int current = 0;

    void Start()
    {
        // set dino group on terrain
        transform.position = new Vector3(transform.parent.position.x + transform.localPosition.x, terrain.SampleHeight(transform.parent.position + transform.localPosition), transform.parent.position.z + transform.localPosition.z);
    }

    void Update()
    {
        // check if dino group is at target position
        if (transform.position != targets[current].position)
        {

            //first rotate dino so that it looks at target
            transform.LookAt(targets[current].position);
            
            // get transform position here to change it later (individual components cannot be changed)
            Vector3 position = transform.position;

            // set y position of position to height of terrain;
            position.y = terrain.SampleHeight(transform.position);

            // than move forward
            position += transform.forward * speed * Time.deltaTime;
            
            // apply position
            transform.position = position;

        }
        else current = (current + 1) % targets.Length; // step to the next target

    }
}
