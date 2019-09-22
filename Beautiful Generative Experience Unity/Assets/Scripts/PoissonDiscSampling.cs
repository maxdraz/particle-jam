using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling
{

    public static List<Vector2> GeneratePoints(float radius, Vector2 worldSize, int samplesBeforeRejection = 30)
    {
        //find the width and height of each cell given diagonal radius
        float cellSize = radius / Mathf.Sqrt(2);
        //create array that holds grid references
        int[,] grid = new int[Mathf.CeilToInt(worldSize.x/cellSize), Mathf.CeilToInt(worldSize.y / cellSize)];

        //create list of points. If a point meets criteria, it will be added. Else another find another.
        List<Vector2> points = new List<Vector2>();
        //list of spawn points contains validated points for spawning
        List<Vector2> spawnPoints = new List<Vector2>();

        spawnPoints.Add(worldSize / 2);

        while(spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnCentre = spawnPoints[spawnIndex];

            for(int i = 0; i < samplesBeforeRejection; i++)
            {
                float angle = Random.value * Mathf.PI * 2; //returns random angle between 0 and 2PI in radians
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
            }
        }

        return null;
               
    }
}
