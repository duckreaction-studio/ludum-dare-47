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
    private LayerMask layerMask;
    void Start()
    {
        fpsCamera = GetComponent<Camera>();
        layerMask = LayerMask.GetMask("CowTarget");

        playerInputController.inputActions.Player.Fire.performed += OnFire;
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
            {
                Debug.Log("Get a cow");
                Cow cow = raycastHit.transform.GetComponentInParent<Cow>();
                if(cow)
                {
                    cow.Die();
                }
            }
        }
    }

    void Update()
    {
        
    }
}
