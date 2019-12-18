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
    public enum ControlMode { CONTROLLER, AUDIO_REACTIVE };
    public ControlMode controlMode = ControlMode.CONTROLLER;

    [Header("General")]
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material mat;
    [SerializeField] private int maxParticles = 1000;
    [SerializeField] private float turnFraction = 1.618034f;
    [SerializeField] private float radius = 0.038f;
    [SerializeField] private List<float> quickAccessTurnFractions;

    [Header("Turning Speeds and Variables (L stick + LT or RT)")]
    public float speedSlow = 0.0001f;
    public float speedNormal = 0.01f;
    public float speedFast = 1f;
    [SerializeField] private float speedCurrent = 0.01f;
    [Range(0,1)][SerializeField] public float smoothing = 0.007f;
    public List<float3> points;

    [Header("Audio Reactivity Variables (LB + X to enter this mode)")]
    [SerializeField] private int bpm =99;
    [SerializeField] private int beatsPerBar = 4;
    [SerializeField] private int rotateAfterBars = 4;


   
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

       // StartCoroutine(RotateWithBPM(AudioAnalyser.GetInstance().songBPM, 4,4));


    }

    private IEnumerator RotateWithBPM(int bpm, int beatsInBar, int numBars)
    {
        while (true)
        {

            float timeDelay = (60f / (float)bpm) * ((float)beatsInBar * (float)numBars); //code will execute every number of seconds it takes to play 4 bars in the song's tempo

            if (controlMode == ControlMode.AUDIO_REACTIVE)
            {
                yield return new WaitForSeconds(timeDelay);

                TurnClockwise();
            }
            else
            {
                break;
            }
        }
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

       if (controlMode == ControlMode.AUDIO_REACTIVE)
       {
          ReactToAudio();
       }
      
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

        SwitchModes();
        ControlMusic();
        ChangeTurnSpeed();
        ExitApplication();
        CustomTurnFractions();
    }

    private void TurnClockwise()
    {
        turnFraction *= 1 + speedCurrent* Time.deltaTime;
    }

    private void TurnAnticlockwise()
    {
        turnFraction *= 1 - speedCurrent* Time.deltaTime;
    }

    private void SwitchModes()
    {
        
        if(Input.GetButton("Left Bumper") && Input.GetButtonDown("X") || Input.GetKeyDown(KeyCode.Return))
        {
           if(controlMode == ControlMode.CONTROLLER)
            {
                controlMode = ControlMode.AUDIO_REACTIVE;
                StartCoroutine(RotateWithBPM(bpm, beatsPerBar, rotateAfterBars));
            }
            else
            {
                controlMode = ControlMode.CONTROLLER;
            }
        }
    }

    private void ControlMusic()
    {
        // Playing and pausing
        if(Input.GetButton("Left Bumper") && Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Space))
        {
            AudioAnalyser.GetInstance().TogglePlayback();
        }

        // Stopping
        if (Input.GetButton("Left Bumper") && Input.GetButtonDown("B") || Input.GetKey(KeyCode.Backspace))
        {
            print("here");
            AudioAnalyser.GetInstance().StopPlayback();
        }
    }

    private void ReactToAudio()
    {
        
        radius = 0.0381f + (0.1f * AudioAnalyser.freqBands[1]);
    }

    private void ChangeTurnSpeed()
    {
        if (Input.GetAxis("Triggers") > 0 || Input.GetKey(KeyCode.LeftShift)){
            //go faster
            speedCurrent = speedFast;
        }
        else if (Input.GetAxis("Triggers") < 0 || Input.GetKey(KeyCode.LeftControl))
        {
          //go slow
         speedCurrent = speedSlow;
        }
        else
        {
            speedCurrent = speedNormal;
        }
    }

    private void ExitApplication()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void CustomTurnFractions()
    {
        if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("pressed 1");
            turnFraction = quickAccessTurnFractions[0];
        }

        if (Input.GetButtonDown("X") || Input.GetKeyDown(KeyCode.Alpha2))
        {
            turnFraction = quickAccessTurnFractions[1];
        }

        if (Input.GetButtonDown("B") || Input.GetKeyDown(KeyCode.Alpha3))
        {
            turnFraction = quickAccessTurnFractions[2];
        }

    }


    public void UpdateBPM()
    {
        bpm = Mathf.RoundToInt(BPMFinder.bpm);
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
