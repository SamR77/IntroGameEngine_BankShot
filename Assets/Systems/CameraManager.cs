using Unity.Cinemachine;
using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class CameraManager : MonoBehaviour
{
    [Header("Manager References")]
    public InputManager inputManager;

    public Transform target;
    [SerializeField] private Vector2 rotationInput;
    [SerializeField] private Vector2 zoomInput;

    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float rotationSpeed = 40f;

    [SerializeField] private float minVerticalAngle = 5f;
    [SerializeField] private float maxVerticalAngle = 50f;

    [Header("Rotation Settings")]
    [SerializeField] public float horizontalLookSensitivity = 30;
    [SerializeField] public float verticalLookSensitivity = 30;

    public CinemachineCamera ballCamera;
    public CinemachineOrbitalFollow orbital;

    public CinemachineCamera MenuCamera;

    private float targetZoom;
    private float currentZoom;


    private void Awake()
    {
        // check and set all references to the managers
        if (inputManager == null) { GameManager.Instance.GetComponentInChildren<InputManager>(); }

        orbital = ballCamera.GetComponent<CinemachineOrbitalFollow>();

        targetZoom = currentZoom = orbital.Radius;


    }


    void LateUpdate()
    {
        HandleRotation();
        HandleZoom();
    }


    public void HandleZoom()
    {
        if (zoomInput.y != 0)
        {
            if (orbital != null)
            { 
                targetZoom = Mathf.Clamp(orbital.Radius - zoomInput.y * zoomSpeed, minDistance, maxDistance);

                zoomInput = Vector2.zero;

            }
        }
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        orbital.Radius = currentZoom;
    }

    public void HandleRotation()
    {
        float lookX = rotationInput.x * horizontalLookSensitivity * Time.deltaTime;
        float lookY = rotationInput.y * verticalLookSensitivity * Time.deltaTime;


        if (rotationInput.magnitude != 0)
        {
            // Horizontal rotation (Y-axis)
            orbital.HorizontalAxis.Value += rotationInput.x * rotationSpeed * Time.deltaTime;

            // Vertical rotation (X-axis)
            orbital.VerticalAxis.Value += -rotationInput.y * rotationSpeed * Time.deltaTime;


            // Clamp vertical rotation
            orbital.VerticalAxis.Value = Mathf.Clamp(orbital.VerticalAxis.Value, minVerticalAngle, maxVerticalAngle);


        }

        //rotationInput = Vector2.zero; // Reset after applying



    }

    private void SetZoomInput(Vector2 input)
    { 
        zoomInput = new Vector2(input.x, input.y);
    }


    private void SetRotationInput(Vector2 input)
    {
        rotationInput = new Vector2(input.x, input.y);
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
