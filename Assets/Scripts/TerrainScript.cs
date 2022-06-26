using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS THE SCRIPT FOR THE TERRAIN
public class TerrainScript : MonoBehaviour
{
    //Instance of the LevelSystem
    public LevelSystem levelSys;
    //terrain
    public Terrain terrain;

    private void OnTriggerEnter(Collider other)
    {
        //if hittet by asteroid, increase the fail in the levelsystem
        if (other.GetComponent<Collider>().tag == "Asteroid")
        {
            levelSys.IncreaseFails();
        }
    }

}
