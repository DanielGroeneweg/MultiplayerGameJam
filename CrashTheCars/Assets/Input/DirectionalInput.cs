using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    [DefaultExecutionOrder(101)]
    public class DirectionalInput : MonoBehaviour
    {
        public static DirectionalInput Instance { get; private set; }
        private Controls _controls => InputManager.Controls;

        public Vector2 DirectionalInputLeft { get; private set; }
        public Vector2 DirectionalInputRight { get; private set; }

        private void Awake()
        {
            SetInstance();
            SetInputActions();
        }

        private void SetInstance()
        {
            if (!Instance) Instance = this;
            else if (Instance != this) Destroy(this);
        }

        private void SetInputActions()
        {
            _controls.Directing.DirectionLeft.performed += SetDirectionalInputLeft;
            _controls.Directing.DirectionLeft.canceled += SetDirectionalInputLeft;

            _controls.Directing.DirectionRight.performed += SetDirectionalInputRight;
            _controls.Directing.DirectionRight.canceled += SetDirectionalInputRight;
        }

        public void Enable()
        {
            enabled = true;

            _controls.Directing.Enable();

            _controls.Directing.DirectionLeft.Enable();
            _controls.Directing.DirectionRight.Enable();
        }

        public void Disable()
        {
            _controls.Directing.DirectionLeft.Disable();
            _controls.Directing.DirectionRight.Disable();

            _controls.Directing.Disable();

            enabled = false;
        }

        private void OnEnable() { Enable(); }

        private void OnDisable() { Disable(); }

        private void SetDirectionalInputLeft(InputAction.CallbackContext context)
        {
            DirectionalInputLeft = context.ReadValue<Vector2>();
        }

        private void SetDirectionalInputRight(InputAction.CallbackContext context)
        {
            DirectionalInputRight = context.ReadValue<Vector2>();
        }
    }

}
