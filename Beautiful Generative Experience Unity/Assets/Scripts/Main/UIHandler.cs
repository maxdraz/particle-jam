using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pointer;
    [SerializeField] private bool displayMenu;
    private Quaternion startRotation;

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
        DisableMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayMenu)
        {

        }
        else
        {
            ResetMenu();
        }
    }

    public void ToggleMenu()
    {       
        displayMenu = !displayMenu;
    }

    private void EnableMenu()
    {
        menu.SetActive(true);
        HandleInput();
    }

    private void DisableMenu()
    {

    }

    private void ResetMenu()
    {
       
    }

    private void HandleInput()
    {
        RotatePointer();
        HandleZooming();
    }

    private void RotatePointer()
    {
        Quaternion pointerRotation = pointer.transform.rotation;


    }

    private void HandleZooming()
    {

    }
}
