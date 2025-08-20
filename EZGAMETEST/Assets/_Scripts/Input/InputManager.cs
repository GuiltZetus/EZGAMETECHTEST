using System;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public UnityEvent OnTapEvent;
    public UnityEvent OnHoldEvent;
    public UnityEvent OnSwipeEvent;
    public UnityEvent OnSwipeLeftEvent;
    public UnityEvent OnSwipeRightEvent;

    private PlayerInput playerInput;
    private PlayerController playerController;

    private InputAction Punch;
    private InputAction Dodge;
    private InputAction Block;
    private InputAction Pointer;

    private Vector2 touchStartPos;
    private float touchStartTime;
    private bool isPressing;
    private bool isHoldingHandled;

    public List<string> actions;


    // Thresholds
    [SerializeField] private float swipeDistanceThreshold = 50f; // pixels
    [SerializeField] private float swipeTimeThreshold = 0.5f;    // seconds
    [SerializeField] private float holdTimeThreshold = 0.5f;    // seconds
    [SerializeField] private float swipeDirectionThreshold = 0.5f; // how horizontal the swipe must be (0 = any, 1 = perfectly horizontal)

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();

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
        isPressing = true;
        isHoldingHandled = false;
    }

    private void Update()
    {
        if (isPressing && !isHoldingHandled && (Time.time - touchStartTime) >= holdTimeThreshold)
        {
            Debug.Log("Hold action detected");
            OnHoldEvent.Invoke();
            isHoldingHandled = true;
            isPressing = false;
        }
    }

    private void OnPressEnded(InputAction.CallbackContext context)
    {
        if (isHoldingHandled)
        {
            return;
        }

        Vector2 touchEndPos = Pointer.ReadValue<Vector2>();
        float touchEndTime = Time.time;
        Vector2 swipeVector = touchEndPos - touchStartPos;
        float distance = swipeVector.magnitude;
        float duration = touchEndTime - touchStartTime;

        if (distance > swipeDistanceThreshold && duration < swipeTimeThreshold)
        {
            float horizontal = swipeVector.x;
            float vertical = swipeVector.y;
            float absHorizontal = Mathf.Abs(horizontal);
            float absVertical = Mathf.Abs(vertical);

            if (absHorizontal > absVertical * swipeDirectionThreshold)
            {
                if (horizontal > 0)
                {
                    Debug.Log("Swipe Right detected");
                    OnSwipeRightEvent.Invoke();
                }
                else
                {
                    Debug.Log("Swipe Left detected");
                    OnSwipeLeftEvent.Invoke();
                }
            }
            else
            {
                Debug.Log("Swipe action detected (not horizontal enough)");
                OnSwipeEvent.Invoke();
            }
            isPressing = false;
        }
        else
        {
            Debug.Log("Tap action detected");
            OnTapEvent.Invoke();
            isPressing = false;
        }
    }
}
