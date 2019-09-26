using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public int number = 1;
    public float turnFraction = 0.5f;
    public float radius;
    public float sphereRadius =0.2f;
    [Range(0,1)]
    public float hue;
    public List<GameObject> objects;

    Material mat;
   
    // Start is called before the first frame update
    void Start()
    {
        // mat = GetComponent<Renderer>().material;
        //CreateObjects(5);
        for (int i = 0; i < 50; i++)
        {

            GameObject sphere = (GameObject)GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector2(1 * i, 0);
            objects.Add(sphere);

        }

       // ColourObjects();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ColourObjects();
        }
        
    }

    void CreateObjects(int count)
    {    

        for(int i = 0; i < count; i++)
        {
            
            GameObject sphere =GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector2(1 * i, 0);
            objects.Add(sphere);
            
        }
    }

    void ColourObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            var material =objects[i].GetComponent<Renderer>().material;
            float h = (float)i / ((float)objects.Count - 1);
            material.color = Color.HSVToRGB(h, 1, 1);
          
        }
    }

    //private void OnDrawGizmos()
    //{
    //    var offset = new Vector2(0.5f,0);
    //    var c = (Vector2)transform.position;
    //    Vector2 p =Vector2.zero;

    //    float increment = (Mathf.PI * 2) * turnFraction;
    //    float a = 0;
    //    int cycles =1;

    //    for (int i=0; i < number; i++)
    //    {
    //        if(a >= Mathf.PI * 2)
    //        {
    //            a = a % Mathf.PI * 2;
    //            cycles++;
    //        }
    //        a = increment * i;
    //        p = new Vector2(Mathf.Cos(a)*radius*cycles, Mathf.Sin(a)*radius*cycles);
    //       // p *= radius;
    //        p += c;
    //        Gizmos.DrawSphere(p, sphereRadius);

    //        print("Actual " +a * Mathf.Rad2Deg+ "Modulus"+(a%(Mathf.PI*2))*Mathf.Rad2Deg);
            
    //    }
    //}
}
