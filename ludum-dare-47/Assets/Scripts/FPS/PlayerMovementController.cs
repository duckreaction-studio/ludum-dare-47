using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravityScale = 1;
    [SerializeField]
    private Transform mainCamera;

    public bool isInAir { get; private set; } = false;

    public PlayerInputController playerInputController;

    private CharacterController characterController;
    float gravity;
   
   
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateJump();

        transform.rotation = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y, 0));

        Vector2 pInput = playerInputController.inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 dir = (transform.right * pInput.x + transform.forward * pInput.y) * walkSpeed;
        dir += transform.up * gravity;
        characterController.Move( dir * Time.deltaTime);
    }

    private void UpdateJump()
    {
        if (isInAir == false)
        {
            if (playerInputController.inputActions.Player.Jump.triggered)
            {
                gravity = jumpForce;
                isInAir = true;
            }
        }
    }

    private void FixedUpdate()
    {
       
        if (characterController.isGrounded)
        {
            isInAir = false;
        }
        else
        {
            isInAir = true;
            gravity += gravityScale*Physics.gravity.y*Time.deltaTime;
        }
      
    }
  
}
