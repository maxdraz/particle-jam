using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms; // for ECS translation component
using Unity.Collections; // for Native Arrays
using Unity.Rendering; // for Render Mesh component
using Unity.Mathematics;

public class TestingECS : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material mat;
    [SerializeField] private int count = 100;
    // Start is called before the first frame update
    void Start()
    {
        EntityManager em = World.Active.EntityManager;
        EntityArchetype playerArchetype = em.CreateArchetype(
            typeof(PlayerStats),
            typeof(Translation),
            typeof(RenderMesh), // this is a SHARED component
            typeof(LocalToWorld) //renderer uses this to compute how it should be visible
            );
      
        NativeArray<Entity> units = new NativeArray<Entity>(count,Allocator.Temp);
        em.CreateEntity(playerArchetype, units); //creates entity based on archetype, and puts it in array

        float cycles =1;
        float turnFraction = 1.61803f;
        float increment = (Mathf.PI * 2) * turnFraction;
        float a = 0f;
        float r = 0.1f;
        for (int i = 0; i < units.Length; i++)
        {
            if(a >= Mathf.PI * 2)
            {
                cycles++;
                a = a % Mathf.PI * 2;                
            }
            a = increment * i;
            Entity p = units[i];
            float3 pos = new float3(Mathf.Cos(a)*r*cycles, Mathf.Sin(a)*r*cycles, 0);
            em.SetComponentData(p, new Translation { Value = pos });
            em.SetSharedComponentData(p, new RenderMesh
            {
                mesh = mesh,
                material = mat
            }) ;
        }
        units.Dispose(); // dispose of native array
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
