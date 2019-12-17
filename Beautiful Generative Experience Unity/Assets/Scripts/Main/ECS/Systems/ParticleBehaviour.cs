using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;



public class ParticleBehaviour : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref Particle particle) =>
        {
            //move particle to its point           
            int particleIndex = particle.index;
            ParticlesECS bootstrapper = ParticlesECS.GetInstance();
            float3 currentPos = translation.Value;
            float3 targetPos = bootstrapper.points[particleIndex];
            
            translation.Value = new float3(math.lerp(currentPos, targetPos, bootstrapper.speed));            

        });
    }
}
