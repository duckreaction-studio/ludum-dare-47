using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
   public enum ADSType
    {
        HoldToAim,
        ClickToAim
    }
    
    [Header("Camera target position")]
    [SerializeField]
    private Transform Target;

    [Header("Camera Aim Settings")]
    [SerializeField]
    private float VerticalSensitivity;
    [SerializeField]
    private float HorizontalSensitivity;
    [SerializeField]
    private float MaxLookUpAngle;
    [SerializeField]
    private float MinLookUpAngle;
    [SerializeField]
    private PlayerInputController playerInputController;

    Vector2 mouseInput;
    Vector2 rotation;
    

    private void Update()
    {
        mouseInput = playerInputController.inputActions.Player.Look.ReadValue<Vector2>();
        rotation.x += mouseInput.x * HorizontalSensitivity;
        rotation.y += mouseInput.y * VerticalSensitivity;
        rotation.y = Mathf.Clamp(rotation.y, MinLookUpAngle, MaxLookUpAngle);
        transform.localRotation = Quaternion.Euler(-rotation.y, rotation.x, 0f);
    }

    void FixedUpdate()
    {
        transform.position = Target.position;
    }

}
