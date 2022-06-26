using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public float flow_speed = 0.2f;
    Renderer renderer;

    void Start()
    {
        // get renderer
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // render texture by offsetting it
        float moveThis = Time.time * flow_speed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(0, moveThis));
    }
}
