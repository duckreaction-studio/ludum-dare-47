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
    private Ray ray;

    void Start()
    {
        fpsCamera = GetComponent<Camera>();
        layerMask = LayerMask.GetMask("CowTarget","Obstacles");

        playerInputController.inputActions.Player.Fire.performed += OnFire;
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
            RaycastHit raycastHit;
            Debug.DrawRay(ray.origin,ray.direction * maxDistance,Color.magenta,10f);
            if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask,QueryTriggerInteraction.Collide))
            {
                Debug.Log("Get something");
                Cow cow = raycastHit.transform.GetComponentInParent<Cow>();
                if(cow)
                {
                    cow.Die();
                }
                else
                {
                    Debug.Log("Is not a cow");
                }
            }
        }
    }

}
