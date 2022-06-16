using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public float speed;
    public AnimationCurve curve;
    public bool bounce = true;
    private float startTime;
    public bool destroy;
    public GameObject goal;
    public int counter;

    public FlyingDino dino;

    // Update is called once per frame
    void Start()
    {
        //random position
        //transform.position = new Vector3(Random.Range(1, 999), Random.Range(250, 350), Random.Range(1, 999));
        goal = this.goal;
        counter = this.counter;
        transform.position = new Vector3(600, 300,600 );
        startTime = Time.time;
        destroy = false;
    }

    void Update()
    {
        //rotation of the object
        goal.transform.Rotate(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);

        //destroy true after 20 seconds
        if (Time.time - startTime > 20)
        {
            destroy = true;
        }

        //destruction: just move the object to a different random position and thereby "destroy" it
        if(destroy == true)
        {
            transform.position = new Vector3(Random.Range(1, 999), Random.Range(250, 350), Random.Range(1, 999));
            destroy = false;
            startTime = Time.time;
        }
    }

 

        /*if (bounce == true)
        {
            transform.position = new Vector3(transform.position.x, curve.Evaluate((Time.time % curve.length)), transform.position.z);
        }
        else
        {
            transform.Translate(0, -40, 0 * Time.deltaTime);
        }*/
    

    private void OnTriggerEnter(Collider other)
    {
        destroy = true;
        counter++;
        dino.Grow();
        //other.transform.localScale += new Vector3(1f, 1f, 1f);
    }

    public int getCounter()
    {
        return counter;
    }
}
        










       /* x = Random.Range(1f, 999f);
        y = Random.Range(250f, 350f);
        z = Random.Range(1f, 999f);

        transform.position = new Vector3(Random.Range(1f, 999f), Random.Range(250f, 350f), Random.Range(1f, 999f));

        Console.log (transform.position.y) ;

        need = obj.transform.position.y;

        if (obj.transform.position.y == need || obj.transform.position.y < (need+10f))
        {
            obj.transform.position = new Vector3(0, 10f, 0) * Time.deltaTime * spee;
        }
        if (transform.position.y == (need + 10))
        {
            obj.transform.position = new Vector3(0, -10f, 0) * Time.deltaTime * spee;
        }
    }*/

