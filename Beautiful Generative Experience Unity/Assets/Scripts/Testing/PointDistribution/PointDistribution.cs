using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDistribution : MonoBehaviour
{
    public int maxPoints = 5;
    public float turnFraction = 1.61f;
    public float radius = 1f;
    public float speed = 0.001f;
    private const float GOLDEN_RATIO = 1.61803399f;
    [Range(0,1)]
    public float smoothing =0.01f;
    public GameObject pointPrefab;
    public List<Vector2> points;
    public List<GameObject> objects;

    private void Start()
    {
        points = GeneratePoints(turnFraction,radius,maxPoints);

        StartCoroutine(DrawPoints(0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            turnFraction = GOLDEN_RATIO;
        }
        HandleInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        UpdatePoints(ref points, turnFraction);
        UpdateObjects(ref objects, points);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ColourObjects(ref objects);
        }
    }

  

    private List<Vector2> GeneratePoints(float turnFraction, float r, int maxIterations)
    {
        List<Vector2> points = new List<Vector2>();
        Vector2 centre = transform.position;        
        float increment = (Mathf.PI * 2) * turnFraction; // 30 degrees 
        int cycles = 1;
        float a =0f;        

        for(int i=0; i< maxIterations; i++)
        {
            if(a >= Mathf.PI*2) // if completed a full turn
            {
                a = a % Mathf.PI * 2;
                cycles++;
                
            }
            a = increment * i;

            Vector2 p;
            
            p = new Vector2(Mathf.Cos(a) * r*cycles, Mathf.Sin(a) * r*cycles);
            p += centre;
            
            points.Add(p);               
        }

        return points;
    }

    private void UpdatePoints(ref List<Vector2> ps, float turnFraction)
    {
        var newpoints = GeneratePoints(turnFraction, radius, ps.Count);
        ps = newpoints;
    }

    private void UpdateObjects(ref List<GameObject> objects, List<Vector2> points)
    {
       for(int i =0; i< objects.Count; i++)
        {
            var currentObj = objects[i];
            var targetPos = points[i];
            var myPos = currentObj.transform.position;

            currentObj.transform.position = Vector2.Lerp(myPos, targetPos, smoothing);
        }
    }   

    private IEnumerator DrawPoints(float t)
    {
        
        //draw spheres at each point
        foreach (Vector2 p in points)
        {
            var point = new Vector2(p.x, p.y);
            //create sphere
            var obj = Instantiate(pointPrefab,transform.position,transform.rotation);
            obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.position = point;            
            objects.Add(obj);
            
            yield return new WaitForSeconds(t);
        }
      
    }

    private void ColourObjects(ref List<GameObject>objs)
    {
        for(int i=0; i< objs.Count; i++)
        {
            var mat = objs[i].GetComponent<Renderer>().material;
            float h = (float)i / ((float)objs.Count - 1);
            mat.color = Color.HSVToRGB(h, 1, 1);
        }
    }

    private void HandleInput(float turnFractionAxis, float radiusAxis)
    {
        if (turnFractionAxis > 0)
        {
            turnFraction *= 1+speed;
        }

        if (turnFractionAxis <0)
        {
            turnFraction *= 1 - speed;
        }

        if(radiusAxis > 0)
        {
            radius *= 1.01f;
        }
        if (radiusAxis < 0)
        {
            radius *= 0.99f;
        }
    }
}
