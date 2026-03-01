using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class CharacterController : MonoBehaviour
    {
        #region Variables

        [Header("Controllers")]

        [SerializeField] private IdleController _idleController;

        [SerializeField] private WalkingController _walkingController;

        [SerializeField] private JumpingController _jumpingController;

        [SerializeField] private CrouchController _crouchController;

        [Header("Parameters")]

        [Tooltip("We don't want the player to move back and forth if the analog stick is angled more than going in the right or left direction. At the same time, we dont want to make it diffuclt to mvoe forward by making the input too closed off. This should be set to a sweet spot.")]
        [SerializeField] private float _analogStickYValueAllowance;

        [Tooltip("We don't want the player to move back and forth if the analog stick is angled left or right more than going down. At the same time, we dont want to make it diffuclt to crouch by making the input too closed off. This should be set to a sweet spot.")]
        [SerializeField] private float _analogStickXValueAllowance;

        [Header("Collider Variables")]

        [SerializeField] private Vector2 _originalColliderScale;

        [SerializeField] private Vector2 _originalColliderPosition;

        [SerializeField] private GameObject _collider;

        [Header("Scripts")]

        [SerializeField] private InputManager _inputManager;

        #endregion

        #region Methods

        // Called every frame in Update()
        private void CallIdle()
        {
            if (_inputManager.GetMoveValue().x <= 0 && _inputManager.GetMoveValue().y == 0 && _jumpingController.IsJumping == false) // Dont want to switch to idle mid jump anim
            {
                _idleController.Idle();
            }
        }

        // Called in FixedUpdate(), as uses RB.MovePosition()
        private void CallWalk()
        {
            if (_crouchController.IsCrouching == false && _jumpingController.IsJumping == false)
            {
                Debug.Log(_inputManager.GetMoveValue());

                // If the player is moving the analog stick to the left or right without angling it upward, move

                if (_inputManager.GetMoveValue().x > 0 && _inputManager.GetMoveValue().y < _analogStickYValueAllowance || _inputManager.GetMoveValue().x < 0 && _inputManager.GetMoveValue().y < _analogStickYValueAllowance)
                {
                    _walkingController.Walk(_inputManager.GetMoveValue());
                }
            }
        }

        // Called every frame in Update()
        private void CallJump()
        {
            // If the left analog stick is flicked up and not angled in the left or right too much, call vertical jump
            if (HasDoneVerticalJumpInput() == true)
            {
                if (_jumpingController.IsJumping == false)
                {
                    _jumpingController.IsJumping = true;
                    _jumpingController.VerticalJump();
                }
            }
            // if we have flicked the analog stick to the upper right or left corners, call horizontal jump
            else if (HasDoneHorizontalJumpInput() == true)
            {
                if (_jumpingController.IsJumping == false)
                {
                    _jumpingController.IsJumping = true;
                    _jumpingController.HorizontalJump(_inputManager.GetMoveValue().x);
                }
            }
        }

        // Called every frame in Update()
        private void CallCrouch()
        {
            if (_jumpingController.IsJumping == false)
            {
                // If the left analog stick is flicked down, and not angled in any direction too far, crouch.
                if (_inputManager.GetMoveValue().y < -0.9f && _inputManager.GetMoveValue().x < _analogStickXValueAllowance || _inputManager.GetMoveValue().y < -0.9f && _inputManager.GetMoveValue().x > -_analogStickXValueAllowance)
                {
                    _crouchController.Crouch();
                }
                // Otherwise, remain the same.
                else
                {
                    _crouchController.Uncrouch();
                }
            }
        }

        private bool HasDoneVerticalJumpInput()
        {
            if (_inputManager.GetMoveValue().y > 0.9f && _inputManager.GetMoveValue().x < _analogStickXValueAllowance || _inputManager.GetMoveValue().y > 0.9f && _inputManager.GetMoveValue().x > -_analogStickXValueAllowance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool HasDoneHorizontalJumpInput()
        {
            // If the input stick is flicked diagonally in the upper right corner, we have inputted the command to jump forward
            if (_inputManager.GetMoveValue().x >= 0.7f && _inputManager.GetMoveValue().x <= 0.9f && _inputManager.GetMoveValue().y >= 0.5f && _inputManager.GetMoveValue().y <= 0.7f)
            {
                return true;
            }
            // If the input stick is flicked diagonally in the upper left corner, we have inputted the command to jump backward
            else if (_inputManager.GetMoveValue().x >= -0.9f && _inputManager.GetMoveValue().x <= -0.7f && _inputManager.GetMoveValue().y >= 0.4f && _inputManager.GetMoveValue().x <= 0.6f)
            {
                return true;
            }
            // Otherwise, we have not inputted any jumping command
            else
            {
                return false;
            }
        }

        public void InitializeCollider()
        {
            _originalColliderScale = _collider.transform.localScale;

            _originalColliderPosition = _collider.transform.localPosition;
        }

        public void ResetCollider()
        {
            _collider.transform.localScale = _originalColliderScale;

            _collider.transform.localPosition = _originalColliderPosition;
        }

        #endregion

        #region Unity Methods

        // Update is called once per frame
        void Update()
        {
            CallIdle();
            CallCrouch();
        }

        private void FixedUpdate()
        {
            CallWalk();
            CallJump();
        }

        private void Start()
        {
            InitializeCollider();
        }

        #endregion
    }
}