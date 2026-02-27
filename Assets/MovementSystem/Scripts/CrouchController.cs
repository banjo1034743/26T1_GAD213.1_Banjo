using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class CrouchController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float _amountToShrinkColliderDown; //0.5

        // Getter and Setter
        public bool IsCrouching 
        {
            get
            {
                return _isCrouching;
            }
            set
            {
                _isCrouching = value;
            }
        } 

        [SerializeField] private bool _isCrouching = false; // Not starting game crouched

        private Vector2 _originalColliderScale;

        [SerializeField] private float _amountToMoveColliderDown; // 0.4

        private Vector2 _originalColliderPosition;

        [Header("Components")]

        [SerializeField] private GameObject _collider;

        [Header("Scripts")]

        [SerializeField] private GroundChecker _groundChecker;

        [SerializeField] private AnimationStateController _animationStateController;

        #endregion

        #region Methods

        public void Crouch()
        {
            if (_groundChecker.IsOnGround() && !_isCrouching)
            {
                Debug.Log("We are crouching");

                // Scale the collider down to the needed size to mostly cover crouching sprite

                _collider.transform.localScale = new Vector2(1, _amountToShrinkColliderDown);

                // Move the scaled collider down to be over the sprite

                _collider.transform.localPosition = new Vector2(0, _amountToMoveColliderDown);

                _animationStateController.ToggleCrouchState();

                _isCrouching = true;
            }
        }

        public void Uncrouch()
        {
            if (_groundChecker.IsOnGround() && _isCrouching)
            {
                Debug.Log("We are not crouching");

                _collider.transform.localScale = _originalColliderScale;

                _collider.transform.localPosition = _originalColliderPosition;

                _animationStateController.ToggleIdleState();

                _isCrouching = false;
            }
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