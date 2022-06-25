using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public LevelSystem level_sys;

    public Terrain terrain;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().tag == "Asteroid")
        {
            level_sys.IncreaseFails();
        }
        //other.transform.localScale += new Vector3(1f, 1f, 1f);
    }

}
