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

        if (other.GetComponent<Collider>().tag == "Fireball")
        {
            GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelSystem>().IncreaseScore();
        }
        Explode();

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
