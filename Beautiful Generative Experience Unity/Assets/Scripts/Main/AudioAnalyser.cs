using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioAnalyser : MonoBehaviour
{
    public static AudioAnalyser instance;
    [SerializeField] private AudioClip musicClip;
    private AudioSource audioSource;   
    public static float[] samples;
    [SerializeField] private bool cubeVisualisation;
    [SerializeField]private GameObject[] cubes;
    [SerializeField] private Material mat;

    public static AudioAnalyser GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
    }

    private void Start()
    {
        samples = new float[64];
        cubes = new GameObject[samples.Length]; 

        if (cubeVisualisation)
        {
            for(int i = 0; i<samples.Length; i++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(0 + (0.5f * i), 0, 0);
                cube.GetComponent<MeshRenderer>().material = mat;
                Material cubeMat = cube.GetComponent<MeshRenderer>().material;                
                cubeMat.color = Color.HSVToRGB(i / samples.Length, i / samples.Length, i / samples.Length);
                cubes[i] = cube;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            GetSpectrumData();
        }
        VisualiseSpectrumData();
    }

    private void GetSpectrumData()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private void VisualiseSpectrumData()
    {
        for(int i = 0; i< samples.Length; i++)
        {
            cubes[i].transform.localScale = new Vector3(1, 1+ (samples[i] * 100f),1);
        }
        
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void OnGUI()
    {
        if (!audioSource.isPlaying)
        {
            if (GUI.Button(new Rect(10, 10, 150, 50), "Play"))
            {
                Play();
            }
        }
        else
        {
            if (GUI.Button(new Rect(10, 10, 150, 50), "Pause"))
            {
                Pause();
            }
        }
        
        if (GUI.Button(new Rect(10, 10 + 50, 150, 50), "Stop"))
        {
            Stop();
        }
    }
}
