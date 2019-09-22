using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour
{
    [Header("Movement")]
    public float mass= 1f;  
    public float moveForceScale = 10f;
    public float maxSpeed = 10f;
    public float maxForce = 10f;
    public float frictionCoefficient = 0.01f;
    private Vector3 force;
    private Vector3 acceleration;
    private Vector3 velocity;

    [Header("Mouse Look")]
    public float sensitivity = 1f;
    [Range(0f, 90f)]
    public float pitchAngleClamp = 60f;
    private float rotX;
    private float rotY;
    float xAxisRotation = 0;

    private void Start()
    {
        //set up vars for rotation
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;

        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        MouseLook();
    }

    void MouseLook()
    {
        rotX += sensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime;
        rotY += sensitivity * Input.GetAxis("Mouse X") * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -pitchAngleClamp, pitchAngleClamp);

        Quaternion localRotation = Quaternion.Euler(-rotX, rotY, 0);
        transform.rotation = localRotation;
    }

    Vector3 InputForce()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        moveDir.Normalize();
        moveDir = transform.TransformDirection(moveDir);

        return moveDir * moveForceScale;
    } 

    Vector3 FrictionForce()
    {
        Vector3 friction = -1 * velocity * (1/frictionCoefficient);

        return friction;
    }

    private void Move()
    {
        force = CalculateNetForce();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
       

        transform.position += velocity * Time.deltaTime;
        
    }

    Vector3 CalculateNetForce()
    {
        Vector3 force = Vector3.zero;
        force = InputForce() + FrictionForce();        
        
        if(force.magnitude > maxForce)
        {
            force = Vector3.ClampMagnitude(force, maxForce);
        }

        return force;
    }   

   
}
