using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandlebrotSet : MonoBehaviour
{
    [SerializeField]
    private float x = 1;
    [SerializeField]
    private float c = 1;
    [SerializeField]
    private float numInterations = 5;
    [SerializeField]
    private List<Vector2> points;
    // Start is called before the first frame update
    void Start()
    {
        points = GenerateMandlebrotPoints(x,c,numInterations);
    }

    private List<Vector2> GenerateMandlebrotPoints(float x, float c, float size)
    {        
        List<Vector2> points = new List<Vector2>();
        float newX = x;
        for(int i = 0; i < size; i++)
        {
            newX = Mathf.Pow(newX, 2);
            float y = newX + c;
            Vector2 point = new Vector2(newX, y);
            points.Add(point);           
        }

        return points;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        for(int i =0; i < points.Count; i++)
        {
            Gizmos.DrawSphere(points[i], 0.5f);
        }
    }
}
