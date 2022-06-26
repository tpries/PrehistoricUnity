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

        // destroy
        Destroy(this.gameObject);
    }

}
