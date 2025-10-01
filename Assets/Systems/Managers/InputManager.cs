using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Inputs.IGameActions, Inputs.ICameraActions
{
    // Reference to the generated Input System class
    private Inputs inputs;


    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] float gamePadSensitivity = 1.0f;

    private InputDevice inputDevice;

    // Input Device type
    public enum InputDeviceType
    {
        KeyboardAndMouse,
        Gamepad,
        Unknown
    }

    private InputDeviceType currentInputDevice;

    // read only public accessor for scripts to check current input device, who may not be subscribed to the event
    public InputDeviceType CurrentInputDevice => currentInputDevice;





    public void Awake()
    {
        // Initialize the Input System
        try
        {
            inputs = new Inputs();

            inputs.Game.SetCallbacks(this); 
            inputs.Game.Enable();

            inputs.Camera.SetCallbacks(this);
            inputs.Camera.Enable();

            // Default assumption: keyboard and mouse
            currentInputDevice = InputDeviceType.KeyboardAndMouse;

        }

        catch (Exception exception)
        {
            Debug.LogError("Error initializing InputManager: " + exception.Message);
        }
    }


    #region Input Events

    // Events triggered when player inputs are detected


    public event Action<Vector2> CameraRotateEvent;
    public event Action<Vector2> CameraZoomEvent;

    public event Action<InputAction.CallbackContext> ShootEvent;

    public event Action PauseEvent;

    public event Action<InputDeviceType> InputDeviceChanged;

    #endregion




    #region Input Callbacks



    public void OnShoot(InputAction.CallbackContext context)
    {
        ShootEvent?.Invoke(context);
    }

    // Camera Input Methods
    public void OnCameraRotate(InputAction.CallbackContext context)
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

            CameraRotateEvent?.Invoke(lookInput);
        
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        float zoomValue = context.ReadValue<float>();
        CameraZoomEvent?.Invoke(new Vector2(0, zoomValue));

    }

    public void OnPause(InputAction.CallbackContext context)
    {
        PauseEvent?.Invoke();
    }













    #endregion




    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed && obj is InputAction action)
        {
            var recentInputDevice = action.activeControl?.device;

            if (recentInputDevice != null && recentInputDevice != inputDevice)
            {
                // set the input device to the lastUsedDevice to stop the check
                inputDevice = recentInputDevice;

                InputDeviceType newInputDevice;

                // Determine the type of inputDevice last used (or last saw activity)
                if (recentInputDevice is Gamepad)
                {
                    newInputDevice = InputDeviceType.Gamepad;
                }

                else if (recentInputDevice is Keyboard || recentInputDevice is Mouse)
                {
                    newInputDevice = InputDeviceType.KeyboardAndMouse;
                }

                else
                {
                      newInputDevice = InputDeviceType.Unknown;                    
                }

                // If the input device has changed, update and invoke the event
                if (newInputDevice != currentInputDevice)
                {
                    currentInputDevice = newInputDevice;
                    InputDeviceChanged?.Invoke(currentInputDevice);
                    // Debug.Log($"Input device changed to: {newInputDevice}");

                }


            }
        }
    }






    void OnEnable()
    {
        if (inputs != null)
        {
            inputs.Game.Enable();
            inputs.Camera.Enable();

            InputSystem.onActionChange += OnActionChange;

        }
    }
    void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Game.Disable();
            inputs.Camera.Disable();

            InputSystem.onActionChange -= OnActionChange;
        }
    }

}
