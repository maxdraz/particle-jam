﻿using System.Collections;
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
    public enum ControlMode { CONTROLLER, AUDIO_REACTIVE };
    public ControlMode controlMode = ControlMode.CONTROLLER;
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material mat;
    [SerializeField] private int maxParticles = 1000;
    [SerializeField] private float turnFraction = 1.618034f;
    [SerializeField] private float radius = 0.038f;
    public float speed = 0.0001f;
    [Range(0,1)][SerializeField] public float smoothing = 0.007f;
    public List<float3> points;   
   
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
        StartCoroutine(SpawnParticles(0.00001f));
        //SpawnParticles();

        StartCoroutine(RotateWithBPM(AudioAnalyser.instance.songBPM, 4,4));


    }

    private void CreateParticle(float3 spawnPos, int i)
    {
        EntityArchetype particleArchetype = entityManager.CreateArchetype(
            typeof(Particle),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld)
        );

        Entity particle = entityManager.CreateEntity(particleArchetype);

        entityManager.SetComponentData(particle, new Particle { index = i });
        entityManager.SetComponentData(particle, new Translation { Value = spawnPos });
        entityManager.SetSharedComponentData(particle, new RenderMesh
        {
            mesh = mesh,
            material = mat
        });

    }

    private IEnumerator SpawnParticles(float delay)
    {
        for (int i = 0; i < points.Count; i++)
        {
            CreateParticle(points[i], i);
            yield return new WaitForSeconds(delay);

        }

    }

    private void SpawnParticles()
    {
        for (int i = 0; i < points.Count; i++)
        {
            CreateParticle(points[i], i);         

        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        UpdatePoints();
        ReactToAudio();
    }

    private void UpdatePoints()
    {
        points = Utilities.GenerateTurnFractionPoints(new float3(0, 0, 0), turnFraction, radius, maxParticles);
    }

    private void HandleInput(float turnFractionAxis, float radiusAxis)
    {
        if (turnFractionAxis > 0)
        {
            TurnClockwise();
        }

        if (turnFractionAxis < 0)
        {
            TurnAnticlockwise();
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

    private void TurnClockwise()
    {
        turnFraction *= 1 + speed;
    }

    private void TurnAnticlockwise()
    {
        turnFraction *= 1 - speed;
    }

    private void SwitchModes()
    {
        //if(Input.ge)
    }

    private void ReactToAudio()
    {
        float currentRadius = radius;
        radius = 0.0381f + (0.1f * AudioAnalyser.freqBands[1]);
    }

    private IEnumerator RotateWithBPM(int bpm, int beatsInBar, int numBars)
    {
        while (true)
        {            
            float timeDelay = (60 / (float)bpm) * (beatsInBar * numBars); //code will execute every number of seconds it takes to play 4 bars in the song's tempo
            print((60/(float)bpm));
            if (controlMode == ControlMode.AUDIO_REACTIVE)
            {
                yield return new WaitForSeconds(9f);
                print("true");
                TurnClockwise();
            }
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