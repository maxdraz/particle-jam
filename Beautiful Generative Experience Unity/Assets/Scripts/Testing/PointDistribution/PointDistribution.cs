using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDistribution : MonoBehaviour
{
    List<Vector2> points;
    public float turnFraction = 1.61f;

    private void Start()
    {
        points = GeneratePoints(turnFraction, 5);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints(points, turnFraction);
    }

    private List<Vector2> GeneratePoints(float turnFraction, int maxIterations)
    {
        List<Vector2> points = new List<Vector2>();
        Vector2 centre = Vector2.zero;        
        float increment = (Mathf.PI * 2) * turnFraction; // 30 degrees 
        float a =0f;

        for(int i=0; i< maxIterations; i++)
        {
            a += increment * i;
            Vector2 p = new Vector2(Mathf.Cos(a), Mathf.Sin(a));
            points.Add(p);               
        }

        return points;
    }

    private void UpdatePoints(List<Vector2> p, float turnFraction)
    {

    }
}
