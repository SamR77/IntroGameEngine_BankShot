using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour, Inputs.IGameplayActions
{
    // Reference to the generated Input System class
    private Inputs inputs;



    public void Awake()
    {
        // Initialize the Input System
        try
        {
            inputs = new Inputs();
            inputs.Gameplay.SetCallbacks(this); // Set the callbacks for the Player action map
            inputs.Gameplay.Enable(); // Enables the "Gameplay" action map
        }
        catch (Exception exception)
        {
            Debug.LogError("Error initializing InputManager: " + exception.Message);
        }
    }

    #region Input Events

    // Events triggered when player inputs are detected
    public event Action<Vector2> CameraLookEvent;

    public event Action ShootEvent;

    #endregion




    #region Input Callbacks

    public void OnCameraLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 lookInput = context.ReadValue<Vector2>();
            CameraLookEvent?.Invoke(lookInput);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started) ShootEvent?.Invoke();
    }


    #endregion







    void OnEnable()
    {
        if (inputs != null)
        {
            inputs.Gameplay.Enable();
        }
    }
    void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Gameplay.Disable();
        }
    }

}
