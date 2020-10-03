using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CowMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float walkSpeed;

    private CharacterController characterController;
   
   
    void Start()
    {
        characterController = GetComponentInChildren<CharacterController>();
    }

    private void Update()
    {

        Vector3 dir = Vector3.zero;
        dir += Physics.gravity;
        characterController.Move( dir * Time.deltaTime);
    }

  
}
