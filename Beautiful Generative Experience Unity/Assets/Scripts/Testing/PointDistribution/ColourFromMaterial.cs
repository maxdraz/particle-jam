using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourFromMaterial : MonoBehaviour
{
    private TrailRenderer trail;
    private Material mat;
    public Gradient grad;
    GradientColorKey[] colors = new GradientColorKey[1];
    GradientAlphaKey[] alphas = new GradientAlphaKey[1];

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
            colors[0].color = mat.color;
            alphas[0].alpha = 255f;
            grad.SetKeys(colors,alphas);
            trail.colorGradient = grad;
        }
    }
}
