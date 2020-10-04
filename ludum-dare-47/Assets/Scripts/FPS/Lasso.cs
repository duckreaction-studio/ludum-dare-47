using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Lasso : MonoBehaviour
{
    [SerializeField]
    private PlayerInputController playerInputController;
    [SerializeField]
    private float maxDistance = 5f;

    private Camera fpsCamera;
    private int layerMask;
    private Ray ray;
    private GameData _gameData;

    void Start()
    {
        fpsCamera = GetComponent<Camera>();
        layerMask = LayerMask.GetMask("Cow", "CowTarget","Obstacles");
        Debug.Log("Mask = " + layerMask);

        playerInputController.inputActions.Player.Fire.performed += OnFire;
    }

    [Inject]
    public void Construct(GameData gameData)
    {
        _gameData = gameData;
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        if (obj.performed && _gameData.running)
        {
            ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
            Debug.DrawRay(ray.origin,ray.direction * maxDistance,Color.magenta,10f);
            var result = Physics.RaycastAll(ray, maxDistance, layerMask);
            if (result.Length > 0)
            {
                Debug.Log("Get something ("+result.Length+")");
                foreach (var hit in result)
                {
                    if (hit.transform.tag == "Cow")
                    {
                        Debug.Log("Get a cow hit collider");
                        Cow cow = hit.transform.GetComponent<Cow>();
                        if (cow)
                        {
                            cow.Die();
                        }
                        else
                        {
                            Debug.LogError("Cannot find component cow in parent");
                        }
                    }
                    else
                    {
                        Debug.Log("Is not a cow "+hit.transform.name,hit.transform);
                    }
                }
            }
        }
    }

}
