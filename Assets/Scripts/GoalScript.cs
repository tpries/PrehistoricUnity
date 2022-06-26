using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS THE CODE OF THE ASTEROIDS
public class GoalScript : MonoBehaviour
{
    //the asteroid
    private GameObject enemy;

    void Start()
    {
       enemy = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        //when a fireball is shooted at the asteroids, let it explode
        //and increase the score in the LevelSystem script
        if (other.GetComponent<Collider>().tag == "Fireball")
        {
            GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelSystem>().IncreaseScore();
            Explode(Random.Range(1, 4));
        }
        else if (other.GetComponent<Collider>().tag == "Terrain")
        {
            Explode(0);
        }
        else
        {
            Explode(Random.Range(1,4));
        }

    }

    void Explode(int index)
    {

        // play explosion sound
        FindObjectOfType<AudioManager>().PlayExplosion(index);

        // explode
        GameObject explosion = (GameObject)Resources.Load("prefabs/Explosion", typeof(GameObject));
        Debug.Log("explode");
        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(expl, 3);

        // destroy the asteroid
        Destroy(this.gameObject);
    }

}
