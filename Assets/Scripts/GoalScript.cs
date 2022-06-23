using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public GameObject obj;
    public LevelSystem level_sys;

    void Start()
    {
        this.obj = obj;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Dino")
        {
            level_sys.IncreaseScore();
            Destroy(this.gameObject);
            //destroy = true;
            //this.goal.transform.position = new Vector3(Random.Range(1, 999), Random.Range(250, 350), Random.Range(1, 999));
            //dino.Grow();
        }

        if (other.GetComponent<Collider>().tag == "Terrain")
        {
            level_sys.IncreaseFails();
            Destroy(this.gameObject);
        }
        //other.transform.localScale += new Vector3(1f, 1f, 1f);
    }


}
