using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction Punch;
    private InputAction Dodge;
    private InputAction Block;
    private InputAction Pointer;

    private Vector2 touchStartPos;
    private float touchStartTime;
    private bool swipeDetected;

    // Thresholds
    private float swipeDistanceThreshold = 50f; // pixels
    private float swipeTimeThreshold = 0.5f;    // seconds

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Punch = playerInput.actions["Tap"];
        Block = playerInput.actions["Hold"];
        Pointer = playerInput.actions["Position"];
    }

    private void OnEnable()
    {
        Punch.started += OnPressStarted;
        Punch.canceled += OnPressEnded;
    }

    private void OnDisable()
    {
        Punch.started -= OnPressStarted;
        Punch.canceled -= OnPressEnded;
    }

    private void OnPressStarted(InputAction.CallbackContext context)
    {
        touchStartPos = Pointer.ReadValue<Vector2>();
        touchStartTime = Time.time;
        swipeDetected = false;
    }

    private void OnPressEnded(InputAction.CallbackContext context)
    {
        Vector2 touchEndPos = Pointer.ReadValue<Vector2>();
        float touchEndTime = Time.time;
        float distance = (touchEndPos - touchStartPos).magnitude;
        float duration = touchEndTime - touchStartTime;

        if (distance > swipeDistanceThreshold && duration < swipeTimeThreshold)
        {
            swipeDetected = true;
            Debug.Log("Dodge (swipe) action performed");
        }
        else
        {
            if (!swipeDetected)
            {
                Debug.Log("Punch (tap) action performed");
            }
        }
    }
}
