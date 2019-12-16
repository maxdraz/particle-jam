using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;
using Unity.Jobs;

public class ParticlesECS : MonoBehaviour
{
    public static ParticlesECS instance;
    private EntityManager entityManager;
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material mat;
    [SerializeField] private int maxParticles = 1000;
    [SerializeField] private float turnFraction = 1.618034f;
    [SerializeField] private float radius = 0.038f;
    [SerializeField] private float speed = 0.0001f;
    [Range(0,1)][SerializeField] public float smoothing = 0.007f;
    [SerializeField]private List<float3> points;
   
    public static ParticlesECS GetInstance() // used to reference the instance from other scripts
    {
        return instance;
    }
    

    private void Awake()
    {        
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        entityManager = World.Active.EntityManager;
     
        //generate points
        points = Utilities.GenerateTurnFractionPoints(new float3(0,0,0), turnFraction, radius, maxParticles);
        //spawn entities at points
        for (int i = 0; i < points.Count; i++)
        {            
            SpawnParticle(points[i]);

        }


    }

    private void SpawnParticle(float3 spawnPos)
    {
        EntityArchetype particleArchetype = entityManager.CreateArchetype(
            typeof(Particle),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld)
        );

        Entity particle = entityManager.CreateEntity(particleArchetype);

        entityManager.SetComponentData(particle, new Translation { Value = spawnPos });
        entityManager.SetSharedComponentData(particle, new RenderMesh
        {
            mesh = mesh,
            material = mat
        });

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        UpdatePoints();
    }

    private void UpdatePoints()
    {
        points = Utilities.GenerateTurnFractionPoints(new float3(0, 0, 0), turnFraction, radius, maxParticles);
    }

    private void HandleInput(float turnFractionAxis, float radiusAxis)
    {
        if (turnFractionAxis > 0)
        {
            turnFraction *= 1 + speed;
        }

        if (turnFractionAxis < 0)
        {
            turnFraction *= 1 - speed;
        }

        if (radiusAxis > 0)
        {
            radius *= 1.01f;
        }
        if (radiusAxis < 0)
        {
            radius *= 0.99f;
        }
    }

    //public class ParticleMoveSystem : JobComponentSystem
    //{
    //    private struct MoveJob : IJobForEachWithEntity<Particle, Translation>
    //    {
    //        //public float dt;
    //        public float3 target;
    //        public float s;
    //        public void Execute(Entity entity, int index, ref Particle particle, ref Translation translation)
    //        {
    //            translation.Value = math.lerp(translation.Value, target, s);
    //        }
    //    }

    //    protected override JobHandle OnUpdate(JobHandle inputDeps)
    //    {
    //        MoveJob job = new MoveJob
    //        {
    //            target = float3.zero,
    //            s = ParticlesECS.instance.smoothing               
    //        };

    //        return job.Schedule(this, inputDeps);
    //    }
    //}

}
