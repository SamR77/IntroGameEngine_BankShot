// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class VFXManager : MonoBehaviour
{
    [Header("Manager References")]

    GameManager gameManager => GameManager.Instance;
    BallManager ballManager => GameManager.Instance.BallManager;
    CameraManager cameraManager => GameManager.Instance.CameraManager;
    GameStateManager gameStateManager => GameManager.Instance.GameStateManager;
    InputManager inputManager => GameManager.Instance.InputManager;
    UIManager uIManager => GameManager.Instance.UIManager;
    VFXManager vFXManager => GameManager.Instance.VFXManager;


    [Header("References")]
    public ParticleSystem LevelCompleteEffect;

    private Coroutine slowMoCoroutine;

    private float velocityScale; 
    private float transitionTime = 3.0f; // Time to transition to TARGET Time

    public void StartBallSlowDownEffect()
    {
        StartCoroutine(SlowMoVelocity(ballManager.rb_ball, velocityScale, transitionTime));
    }


    // trigger slow mo effect, when its complete clear to coroutine
    IEnumerator SlowMoVelocity(Rigidbody ball, float velocityScale, float duration)
    {
        Vector3 originalVelocity = ball.linearVelocity;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float easedTime = 1f - Mathf.Pow(1f - t, 3); // Optional easing

            ball.linearVelocity = originalVelocity * Mathf.Lerp(1f, velocityScale, easedTime);

            elapsed += Time.deltaTime;
            yield return null;
        }
        ball.linearVelocity = originalVelocity * velocityScale;
    }



}
