using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public static class Utilities
{
   public static List<float3> GenerateTurnFractionPoints(float3 centre,float turnFraction, float radius, int maxPoints)
    {
        List<float3> points = new List<float3>();        
        float increment = (math.PI * 2) * turnFraction; // 30 degrees 
        int cycles = 1;
        float a = 0f;

        for (int i = 0; i < maxPoints; i++)
        {
            if (a >= math.PI * 2) // if completed a full turn
            {
                a = a % math.PI * 2;
                cycles++;

            }
            a = increment * i;

            float3 p;

            p = new float3(Mathf.Cos(a) * radius * cycles, Mathf.Sin(a) * radius * cycles, 0f);
            p += centre;

            points.Add(p);
        }

        return points;
    }
}
