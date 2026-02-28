using System;
using System.Collections;
using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class JumpingController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float _playerStandingYPosition = -0.956f; // original value was -1.092f

        [SerializeField] private float _jumpingSpeed = 1f; // Used as frequency for Mathf.sin

        [Tooltip("Force that is appled down on the character when jumping. Can control jumping height with this")]
        [SerializeField] private float _downwardForce = 1f;

        private Vector2 _forceDirection;

        private bool _adjustedPlayerYPos = false;

        private Coroutine _jumpingCoroutine;

        public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }

        [SerializeField] private bool _isJumping = false;

        [Header("Components")]

        [Tooltip("Used for jumping the character")]
        [SerializeField] private Rigidbody2D _rigidBody;

        [Header("Scripts")]

        [SerializeField] private CharacterController _characterController;

        [SerializeField] private AnimationStateController _animationStateController;

        #endregion

        #region Methods

        public void Jump()
        {
            Debug.Log("Jump called");
            Debug.Log("Our Y pos is: " + transform.position.y);
            Debug.Log("The position we want the player to stay at is: " + _playerStandingYPosition);

            if (_isJumping)
            {
                Debug.Log("We're jumping");

                if (!_adjustedPlayerYPos)
                {
                    Debug.Log("We're adjusting the player's y pos");

                    // Raises the player off the ground so we don't trigger the check for if we're at ground level early.
                    // Using MovePosition results in a error for some reason.
                    transform.Translate(0, 0.05f, 0); 

                    _adjustedPlayerYPos = true;
                }

                if (_jumpingCoroutine == null)
                {
                    _jumpingCoroutine = StartCoroutine(MovePlayerDuringJump());       
                }
            }
        }

        private IEnumerator MovePlayerDuringJump()
        {
            float timer = 0f;

            _animationStateController.ToggleJumpVerticalState();

            while ((float)Math.Round(transform.position.y, 3) > (float)Math.Round(_playerStandingYPosition, 3)) // Mathf has no rounding funcitons that round to floats, so the Math class was needed here
            {
                float x = transform.position.x;

                // We don't use Time.time as that continues increasing in value outside of this jumping loop, which will result in the player
                // starting the jump at a random point on the Y axis
                float y = Mathf.Sin(timer * _jumpingSpeed); 

                Vector2 amountToMove = new Vector2(x, y);
                _forceDirection = new Vector2(0, _downwardForce);

                _rigidBody.MovePosition(amountToMove);
                _rigidBody.AddForce(_forceDirection, ForceMode2D.Force);

                timer += Time.deltaTime;

                yield return null;
            }

            Debug.Log("We're stopping the jump code");

            _isJumping = false;
            _adjustedPlayerYPos = false;

            StopCoroutine(_jumpingCoroutine);
            _jumpingCoroutine = null;
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion
    }
}