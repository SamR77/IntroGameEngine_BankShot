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
        lookInput = new Vector2(vector.x, vector.y);
    }



    void OnEnable()
    {
       inputManager.CameraLookEvent += SetCameraLookInput;
    }



    private void OnDestroy()
    {
        inputManager.CameraLookEvent -= SetCameraLookInput;
    }



}
