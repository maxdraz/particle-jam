using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandelbrotController : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float zoomSpeed = 0.5f;
    public float scale =4;

    // Update is called once per frame
    void Update()
    {
        ExploreWithInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetAxis("Mouse ScrollWheel"));
    }

    void ExploreWithInput(float horAxis, float vertAxis, float zoomAxis)
    {
        float prevScale = scale;
        scale +=-1 * zoomAxis * zoomSpeed;
        scale = Mathf.Lerp(prevScale, scale, 1f);
        pos += new Vector2(horAxis, vertAxis)* scale * Time.deltaTime;

        mat.SetVector("_Area", new Vector4(pos.x, pos.y, scale, scale));
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isMouse)
        {
            print(e.delta);
        }
    }
}
