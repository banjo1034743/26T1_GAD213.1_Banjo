using UnityEngine;
using UnityEngine.InputSystem;

namespace GAD213.P1.MovementSystem
{
    public class InputManager : MonoBehaviour
    {
        #region Variables

        [Header("Input System")]

        [SerializeField] private InputActionAsset _inputActionAsset;

        private InputActionMap _inputActionMap;

        // === INPUT ACTIONS ===

        private InputAction _inputActionMove;

        #endregion

        #region Methods

        public Vector2 GetMoveValue()
        {
            return _inputActionMove.ReadValue<Vector2>();
        }

        private void InitializeInputActions()
        {
            _inputActionMap = _inputActionAsset.FindActionMap("Player");

            _inputActionMove = _inputActionMap.FindAction("Move");
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InitializeInputActions();
        }

        #endregion
    }
}