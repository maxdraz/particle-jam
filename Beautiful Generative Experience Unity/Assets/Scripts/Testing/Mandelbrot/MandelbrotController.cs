using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandelbrotController : MonoBehaviour
{
    public bool usingController;
    public Material mat;
    public Vector2 pos;
    public float zoomSpeed = 0.5f;
    public float scale =4;
    private Vector2 smoothPos;
    private float smoothScale;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (usingController)
        {
            HandleInputs(Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"),
                Input.GetAxis("ZoomIn"),
                Input.GetAxis("ZoomOut"));
        }
        else
        {
            HandleInputs(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetAxis("Jump"));
        }
     
        UpdateShader();
    }

    void HandleInputs(float horAxis, float vertAxis, float zoomAxis)
    {
        //Movement
        pos += new Vector2(horAxis, vertAxis) * scale * Time.deltaTime;

        //Zooming
        if (zoomAxis > 0) // scrolling up
        {
            scale *= .99f;
           
        }
        if(zoomAxis < 0) //scrolling down
        {
            scale *= 1.01f* zoomSpeed;
        }
    }

    void HandleInputs(float horAxis, float vertAxis, float zoomInAxis, float zoomOutAxis)
    {
        //Movement
        pos += new Vector2(horAxis, vertAxis) * scale * Time.deltaTime;

        //Zooming
        if (zoomInAxis > 0) // scrolling up
        {
            scale *= .99f;

        }
        if (zoomOutAxis > 0) //scrolling down
        {
            scale *= 1.01f * zoomSpeed;
        }
    }

    void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.03f); //makes smooth pos 0.03f closer to pos
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.03f);

        float aspect = (float)Screen.width / (float)Screen.height;
        float scaleX = smoothScale;
        float scaleY = smoothScale;
        if(aspect > 1) //screen is wider than its tall so scale Y proportionally less
        {
            scaleY /= aspect;
        }
        if(aspect < 1) // screen is taller than it is wider so scale X proportionally less
        {
            scaleX *= aspect;
        }  

        //Set the above to material properties
        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
    }

  
}
