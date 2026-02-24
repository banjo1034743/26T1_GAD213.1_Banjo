using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class CrouchController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float _amountToShrinkColliderDown; //0.5

        private Vector2 _originalColliderScale;

        [SerializeField] private float _amountToMoveColliderDown; // 0.4

        private Vector2 _originalColliderPosition;

        [Header("Components")]

        [SerializeField] private GameObject _collider;

        [Header("Scripts")]

        [SerializeField] private GroundChecker _groundChecker;

        #endregion

        #region Methods

        public void Crouch()
        {
            if (_groundChecker.IsOnGround())
            {
                // Scale the collider down to the needed size to mostly cover crouching sprite

                _collider.transform.localScale = new Vector2(0, _amountToShrinkColliderDown);

                // Move the scaled collider down to be over the sprite

                _collider.transform.position = new Vector2(0, _amountToMoveColliderDown);
            }
        }

        public void Uncrouch()
        {
            _collider.transform.localScale = _originalColliderScale;

            _collider.transform.localPosition = _originalColliderPosition;
        }

        // Called in Start()
        private void InitializeVariables()
        {
            _originalColliderScale = _collider.transform.localScale;

            _originalColliderPosition = _collider.transform.localPosition;
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InitializeVariables();
        }

        #endregion
    }
}