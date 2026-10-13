using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action<Vector2> PointerDown;
    public event Action<Vector2> PointerDragged;
    public event Action<Vector2> PointerUp;

    private InputSystem_Actions input;

    private Camera mainCamera;

    private bool isPressed;

    private void Awake()
    {
        input = new InputSystem_Actions();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Press.started += OnPressStarted;
        input.Player.Press.canceled += OnPressCanceled;
    }

    private void OnDisable()
    {
        input.Player.Press.started -= OnPressStarted;
        input.Player.Press.canceled -= OnPressCanceled;

        input.Disable();
    }

    private void Update()
    {
        if (!isPressed)
            return;

        PointerDragged?.Invoke(GetWorldPointerPosition());
    }

    private void OnPressStarted(InputAction.CallbackContext obj)
    {
        isPressed = true;

        PointerDown?.Invoke(GetWorldPointerPosition());
    }

    private void OnPressCanceled(InputAction.CallbackContext obj)
    {
        isPressed = false;

        PointerUp?.Invoke(GetWorldPointerPosition());
    }

    private Vector2 GetWorldPointerPosition()
    {
        Vector2 screen =
            input.Player.PointerPosition.ReadValue<Vector2>();

        return mainCamera.ScreenToWorldPoint(screen);
    }
}