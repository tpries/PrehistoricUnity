using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private GameObject obj;
    private LevelSystem level_sys;

    void Start()
    {
        obj = this.gameObject;
        level_sys = FindObjectOfType<LevelSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().tag == "Dino")
        {
            level_sys.IncreaseScore();
            Explode();
            //destroy = true;
            //this.goal.transform.position = new Vector3(Random.Range(1, 999), Random.Range(250, 350), Random.Range(1, 999));
            //dino.Grow();
        }

        if (other.GetComponent<Collider>().tag == "Terrain")
        {
            level_sys.IncreaseFails();
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
