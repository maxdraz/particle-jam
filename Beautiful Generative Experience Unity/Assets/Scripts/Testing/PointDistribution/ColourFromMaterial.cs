using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourFromMaterial : MonoBehaviour
{
    private TrailRenderer trail;
    private Material mat;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        mat = GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            trail.startColor = mat.color;
        }
    }
}
