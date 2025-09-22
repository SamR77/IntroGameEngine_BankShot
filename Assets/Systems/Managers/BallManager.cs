// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallManager : MonoBehaviour
{

    [Header("Manager References")]

    GameManager gameManager => GameManager.Instance;
    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    InputManager inputManager => GameManager.Instance.InputManager;        
    UIManager uIManager => GameManager.Instance.UIManager;
    


    [Header("References")]
    public GameObject ballMesh;  
    public Rigidbody rb_ball;
    public GameObject aimGuide;

    public bool ballStopped;
    public float ballMagnitudeStopThreshold = 0.1f; // Adjust this value as needed
    public float ballStopCheckDelay = 0.5f; // Adjust this value as needed
    public float shootForce = 25f;

    [SerializeField, Header("Debug Output (read only)")]
    private float ballVelocityMagnitude;

    private Coroutine checkBallStoppedCoroutine;

    private void Start()
    {
        ballStopped = true; // the ball should be stopped at the start of the game

        //cameraManager.freeLookCamera.Follow = ball.transform;
        //cameraManager.instance.freeLookCamera.LookAt = ball.transform;
    }

    void Update()
    {
        // spits out the ball vector magnitude for debugging    
        ballVelocityMagnitude = rb_ball.linearVelocity.magnitude;
    }

    public void ShootBall(InputAction.CallbackContext context) // adds force to ball in a direction away from camera
    {
        if (context.started)
        {
            gameManager.shotsRemaining -= 1;            
            uIManager.GameplayUIController.UpdateShotsRemainingLabel();            

            ballStopped = false; // the ball should be moving at this point
            rb_ball.AddForce(aimGuide.transform.forward * shootForce, ForceMode.VelocityChange);

            gameStateManager.SwitchToState(GameState_Rolling.Instance);
        }        
    }

    // called during Rolling state to check if the ball has stopped moving after a short delay
    // if so it calls CheckForRemainingShots in GameManager to determine if the player failed or can continue

    public void StartCheckBallStoppedAfterDelay()
    {
        checkBallStoppedCoroutine = StartCoroutine(CheckBallStoppedAfterDelay());
    }

    public void StopCheckBallStoppedAfterDelay()
    {
        if (checkBallStoppedCoroutine != null)
        {
            StopCoroutine(checkBallStoppedCoroutine);
            checkBallStoppedCoroutine = null; // Important: set to null after stopping
        }
    }

    public IEnumerator CheckBallStoppedAfterDelay()
    {
        yield return new WaitForSeconds(ballStopCheckDelay);

        float stillTime = 0f;
        float requiredStillTime = 1.5f; // Time the ball must remain below threshold
        float checkInterval = 0.1f;

        while (true)
        {
            if (rb_ball.linearVelocity.magnitude < ballMagnitudeStopThreshold &&
                rb_ball.angularVelocity.magnitude < ballMagnitudeStopThreshold)
            {
                stillTime += checkInterval;

                if (stillTime >= requiredStillTime)
                {
                    StopBall();
                    ballStopped = true;

                    // Add your GameState_LevelComplete check here
                    gameManager.CheckForRemainingShots();
                    yield break;
                }
            }
            else
            {
                stillTime = 0f; // Reset if ball starts moving again
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoalTrigger")
        {
            Debug.Log("Goal Reached");
            gameManager.CheckForGameWin();
            return;
        }

        else if (other.gameObject.tag == "ResetTrigger")
        {
            SetBallToStartPosition();
        }
    }

    public void HandleAimGuide()
    {
        // Get the direction the camera is facing, constrained to the Y-axis only
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;  // Ensure no vertical rotation is applied (Y-axis locked)

        // Calculate the new rotation that matches the camera's facing direction
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

        // Smoothly interpolate between the current rotation and the target rotation
        aimGuide.transform.rotation = Quaternion.Slerp(aimGuide.transform.rotation, targetRotation, Time.deltaTime * 50f);
    }

    public void SetBallToStartPosition()
    {
        //  Find Start Position object in current scene

        Transform startPosition = GameObject.FindWithTag("BallSpawnPoint").transform;       

        StopBall(); // Stop the ball   
        rb_ball.position = startPosition.transform.position;
        rb_ball.rotation = startPosition.transform.rotation;


        // ** Bugfix by Daniel Nascimento **
        // When setting position and rotation in the rigidbody
        // it'll take effect during the physics update. So I believe
        // when cinemachine tries to get the rotation to adjust the camera
        // the rotation hasn't changed yet. So I'm setting it in the
        // transform here as well.

        rb_ball.transform.position = startPosition.transform.position;
        rb_ball.transform.rotation = startPosition.transform.rotation;

        rb_ball.GetComponent<TrailRenderer>().Clear();

        cameraManager.SetBallCameraOrientation(startPosition.transform.forward);
    }

    public void StopBall() //immediately halts the ball movement
    {
        rb_ball.isKinematic = true;
        rb_ball.isKinematic = false;
        // Also setting velocities to zero just in case.
        rb_ball.linearVelocity = Vector3.zero;
        rb_ball.angularVelocity = Vector3.zero;
    }
}
