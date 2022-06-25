using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private GameObject obj;

    void Start()
    {
        obj = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().tag == "Dino")
        {
            Explode();
            //destroy = true;
            //this.goal.transform.position = new Vector3(Random.Range(1, 999), Random.Range(250, 350), Random.Range(1, 999));
            //dino.Grow();
        }

        if (other.GetComponent<Collider>().tag == "Terrain")
        {
            Explode();
        }
        //other.transform.localScale += new Vector3(1f, 1f, 1f);
    }

    void Explode()
    {

        // play explosion sound
        FindObjectOfType<AudioManager>().PlayExplosion(Random.Range(0, 4));

        // explode
        GameObject explosion = (GameObject)Resources.Load("prefabs/Explosion", typeof(GameObject));
        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(expl, 3);

        // destroy
        Destroy(this.gameObject);
    }

}
