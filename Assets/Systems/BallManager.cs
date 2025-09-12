using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private InputManager inputManager;

    [SerializeField] private Vector2 lookInput;


    private void Awake()
    {
        inputManager = GameManager.Instance.inputManager;
        if (inputManager == null)
        {
            Debug.LogError("InputManager not found.");
            return;
        }
    }





    private void SetCameraLookInput(Vector2 vector)
    {
       
    }









}
