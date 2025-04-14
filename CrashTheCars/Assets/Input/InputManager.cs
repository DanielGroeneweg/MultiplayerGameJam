using UnityEngine;

namespace Player.Input
{
    [DefaultExecutionOrder(100)]
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public static Controls Controls { get; private set; }

        private void Awake()
        {
            SetInstance();
            SetControls();
        }

        private void SetInstance()
        {
            if (!Instance) Instance = this;
            else if (Instance != this) Destroy(this);
        }

        private void SetControls() { Controls ??= new(); }

        public void Enable()
        {
            enabled = true;

            DirectionalInput.Instance.Enable();
        }

        public void Disable()
        {
            DirectionalInput.Instance.Disable();

            enabled = false;
        }

        private void OnEnable() { Enable(); }

        private void OnDisable() { Disable(); }
    }

}
