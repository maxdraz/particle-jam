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
    public static float[] freqBands;
    [SerializeField] private bool cubeVisualisation;
    [SerializeField]private GameObject[] cubes;
    [SerializeField] private Material mat;
    [SerializeField] public int songBPM = 99;

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
        samples = new float[512];
        freqBands = new float[8];
        cubes = new GameObject[freqBands.Length]; 

        if (cubeVisualisation)
        {
            for(int i = 0; i<freqBands.Length; i++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(transform.position.x + (2f * i), transform.position.y, transform.position.z);
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
            MakeFrequencyBands();
        }
        VisualiseSpectrumData();
    }

    private void GetSpectrumData()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        int count = 0;

        for(int i=0; i <8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for(int j=0; j <sampleCount; j++)
            {
                average += samples[count] * (count +1);
                count++;
            }

            average /= count;

            freqBands[i] = average * 10;
            
        }
    }

    private void VisualiseSpectrumData()
    {
        for(int i = 0; i< freqBands.Length; i++)
        {
            cubes[i].transform.localScale = new Vector3(1, 1+ (freqBands[i] * 10f),1);
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
