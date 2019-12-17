using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BPMFinder : MonoBehaviour
{
    [SerializeField] private float timeTaken =0f;
    [SerializeField] private float beatsPerBar =4;
    [SerializeField] public static float bpm;
    public static UnityEvent OnBPMFound;

    private void OnDisable()
    {
        OnBPMFound.RemoveAllListeners();
    }
    // Start is called before the first frame update
    void Start()
    {
        OnBPMFound = new UnityEvent();
        OnBPMFound.AddListener(ParticlesECS.instance.UpdateBPM);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Right Bumper"))
        {
            
            timeTaken += Time.deltaTime;            
        }
        else
        {
            
            if (timeTaken > 0)
            {
                bpm = CalculateBPM();
                timeTaken = 0;
                OnBPMFound.Invoke();

            }
            //else
            //{
            //    return;
            //}
        }
    }
   
    private float CalculateBPM()
    {
        return (beatsPerBar / timeTaken) * 60;
    }
}
