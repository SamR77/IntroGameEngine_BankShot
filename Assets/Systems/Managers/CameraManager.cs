using Unity.Cinemachine;
using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class CameraManager : MonoBehaviour
{
    [Header("Manager References")]
    GameManager gameManager => GameManager.Instance;
    InputManager inputManager => GameManager.Instance.InputManager;

    [Header("Cinemachine Camera Refereces")]
    [SerializeField] private CinemachineCamera ballCamera;    
    [SerializeField] private CinemachineCamera menuCamera;
    [SerializeField] private Transform target;

    private CinemachineOrbitalFollow ballCamOrbitalFollow;


    [Header("Rotation Settings")]
    [SerializeField] public float horizontalLookSensitivity = 30;
    [SerializeField] public float verticalLookSensitivity = 30;

    [SerializeField] private float minVerticalAngle = 15f;
    [SerializeField] private float maxVerticalAngle = 60f;
    [SerializeField] private float rotationSpeed = 40f;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;

    private float targetZoom;
    private float currentZoom;

    // Input value references
    [SerializeField] private Vector2 rotationInput;
    [SerializeField] private Vector2 zoomInput;

    private void Awake()
    {
        // check and set all references to the managers
        // if (inputManager == null) { GameManager.Instance.GetComponentInChildren<InputManager>(); }

        ballCamOrbitalFollow = ballCamera.GetComponent<CinemachineOrbitalFollow>();

        targetZoom = currentZoom = ballCamOrbitalFollow.Radius;


    }


    void LateUpdate()
    {
        //HandleRotation();
        //HandleZoom();
    }

    // Call these in Late Update during Aim_State
    public void HandleZoom()
    {
        if (zoomInput.y != 0)
        {
            if (ballCamOrbitalFollow != null)
            { 
                targetZoom = Mathf.Clamp(ballCamOrbitalFollow.Radius - zoomInput.y * zoomSpeed, minDistance, maxDistance);

                zoomInput = Vector2.zero;

            }
        }
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        ballCamOrbitalFollow.Radius = currentZoom;
    }
    public void HandleRotation()
    {
        float lookX = rotationInput.x * horizontalLookSensitivity * Time.deltaTime;
        float lookY = rotationInput.y * verticalLookSensitivity * Time.deltaTime;


        if (rotationInput.magnitude != 0)
        {
            // Horizontal rotation (Y-axis)
            ballCamOrbitalFollow.HorizontalAxis.Value += rotationInput.x * rotationSpeed * Time.deltaTime;

            // Vertical rotation (X-axis)
            ballCamOrbitalFollow.VerticalAxis.Value += -rotationInput.y * rotationSpeed * Time.deltaTime;


            // Clamp vertical rotation
            ballCamOrbitalFollow.VerticalAxis.Value = Mathf.Clamp(ballCamOrbitalFollow.VerticalAxis.Value, minVerticalAngle, maxVerticalAngle);


        }

        //rotationInput = Vector2.zero; // Reset after applying



    }

    public void EnableMenuCamera()
    {
        ballCamera.enabled = false;
        menuCamera.enabled = true;
    }

    public void EnableBallCamera()
    {
        ballCamera.enabled = true;
        menuCamera.enabled = false;
    }

    public void DisableCameraOrbit()
    {
        ballCamera.enabled = false;
    }

    public void EnableCameraOrbit()
    {
        ballCamera.enabled = true;
    }

    public void SetBallCameraOrientation(Vector3 targetOrientation)
    {
        // Snap camera to target's position and orientation
        ballCamera.ForceCameraPosition(target.position, Quaternion.LookRotation(targetOrientation));



    }








    private void SetZoomInput(Vector2 input)
    { 
        zoomInput = new Vector2(input.x, input.y);
    }


    private void SetRotationInput(Vector2 input)
    {
        rotationInput = new Vector2(input.x, input.y);
    }

    public void DisableAllCameras()
    {
        ballCamera.enabled = false;
        menuCamera.enabled = false;
    }

    public void ResetCameraPosition()
    {
        Debug.Log("Called Reset Camera Position in CameraManager, Logic not yet set");

        //var offset = freeLookCamera.LookAt.rotation * new Vector3(0, 0, -14);

        //freeLookCamera.ForceCameraPosition(freeLookCamera.LookAt.position + offset, freeLookCamera.LookAt.rotation);
        //freeLookCamera.m_YAxis.Value = 0.5f;
    }



    void OnEnable()
    {
        inputManager.RotateCameraEvent += SetRotationInput;
        inputManager.ZoomCameraEvent += SetZoomInput;
    }
    private void OnDestroy()
    {
        inputManager.RotateCameraEvent -= SetRotationInput;
        inputManager.ZoomCameraEvent -= SetZoomInput;
    }


}
