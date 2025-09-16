using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Inputs.IGameActions, Inputs.ICameraActions
{
    // Reference to the generated Input System class
    private Inputs inputs;


    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] float gamePadSensitivity = 1.0f;

    public void Awake()
    {
        // Initialize the Input System
        try
        {
            inputs = new Inputs();

            inputs.Game.SetCallbacks(this); // Set the callbacks for the Ball action map
            inputs.Game.Enable(); // Enables the "Ball" action map

            inputs.Camera.SetCallbacks(this); // Set the callbacks for the Camera action map
            inputs.Camera.Enable(); // Enables the "Camera" action map

        }
        catch (Exception exception)
        {
            Debug.LogError("Error initializing InputManager: " + exception.Message);
        }
    }

    #region Input Events

    // Events triggered when player inputs are detected
    
    
    public event Action<Vector2> RotateCameraEvent;
    public event Action<Vector2> ZoomCameraEvent;

    public event Action<InputAction.CallbackContext> ShootEvent;

    public event Action<InputAction.CallbackContext> JumpEvent;
    public event Action PauseEvent;

    #endregion




    #region Input Callbacks



    public void OnShoot(InputAction.CallbackContext context)
    {
        ShootEvent?.Invoke(context);
    }

    // Camera Input Methods
    public void OnRotateCamera(InputAction.CallbackContext context)
    {  

            Vector2 lookInput = context.ReadValue<Vector2>();
            var device = context.control.device;

            if (device is Mouse)
            {
                lookInput *= mouseSensitivity; // Scale down mouse input for finer control
            }

            if (device is Gamepad)
            {
                lookInput *= gamePadSensitivity; // Scale down gamepad input for finer control
            }

            RotateCameraEvent?.Invoke(lookInput);
        
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        PauseEvent?.Invoke();
    }

    public void OnMouseZoom(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ZoomCameraEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnGamepadZoom(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ZoomCameraEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }





    #endregion







    void OnEnable()
    {
        if (inputs != null)
        {
            inputs.Game.Enable();
            inputs.Camera.Enable();
        }
    }
    void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Game.Disable();
            inputs.Camera.Disable();
        }
    }

}
