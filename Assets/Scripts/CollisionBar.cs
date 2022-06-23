using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionBar : MonoBehaviour
{
    //For the collisionline
    public Image collisions;
    public LevelSystem lev;
    // Start is called before the first frame update

    public void UpdateCollisions()
    {
        collisions.fillAmount = Mathf.Clamp(lev.life / lev.maxLifes, 0, 1f);
    }
}
