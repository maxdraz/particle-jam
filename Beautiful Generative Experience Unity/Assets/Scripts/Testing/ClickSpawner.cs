using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public LayerMask canHit;
    public GameObject pObjToSpawn;
    private GameObject objToPlace;
    
    
    // Start is called before the first frame update
    void Start()
    {
        var initialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objToPlace = Instantiate(pObjToSpawn, initialPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objToPlace.layer = 8;
            objToPlace = Instantiate(pObjToSpawn, GetPlacementPos(), Quaternion.identity);
        }
        objToPlace.transform.position = Vector3.Lerp(objToPlace.transform.position, GetPlacementPos(), 0.8f);
        //print(GetPlacementPos());

        print("WORLD " +Camera.main.ScreenToWorldPoint(Input.mousePosition));
        print(Input.mousePosition);
    }

    Vector3 GetPlacementPos()
    {
        Ray ray= new Ray();
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;// make it so the ray goes from cam centre and shoots forward

        ray.origin = Camera.main.transform.position;
        ray.direction = Utilities.GetDir(ray.origin, Camera.main.ScreenToWorldPoint(mousePos));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, canHit)){ 
            return hit.point + hit.normal * 0.5f;
        }
        else {
            return objToPlace.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(Camera.main.transform.position,
            Utilities.GetDir(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)));

        //Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.5f);

        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
        dir.Normalize();
        dir *= 5f;
        Debug.DrawRay(Camera.main.transform.position, dir);
    }
}
