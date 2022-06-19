using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingDino : MonoBehaviour
{
    public Terrain terrain;

    public Transform[] targets;
    public float speed;

    private int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.parent.position.x + transform.localPosition.x, terrain.SampleHeight(transform.parent.position + transform.localPosition), transform.parent.position.z + transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {

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
        else current = (current + 1) % targets.Length;

    }
}
