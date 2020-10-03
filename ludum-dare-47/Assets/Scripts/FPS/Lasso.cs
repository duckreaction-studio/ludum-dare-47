using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lasso : MonoBehaviour
{
    [SerializeField]
    private PlayerInputController playerInputController;
    [SerializeField]
    private float maxDistance = 5f;

    private Camera fpsCamera;
    private int layerMask;
    void Start()
    {
        fpsCamera = GetComponent<Camera>();
        layerMask = LayerMask.NameToLayer("CowTarget");

        playerInputController.inputActions.Player.Fire.performed += OnFire;
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Debug.Log("Fire");
            Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
            {
                Debug.Log("Get a cow");
            }
        }
    }

    void Update()
    {
        
    }
}
