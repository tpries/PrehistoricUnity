using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public float flow_speed = 0.2f;
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveThis = Time.time * flow_speed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(0, moveThis));
    }
}
