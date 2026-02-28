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

        private bool _adjustedPlayerYPos = false;

        private Coroutine _jumpingCoroutine;

        public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }

        [SerializeField] private bool _isJumping = false;

        [Header("Components")]

        [Tooltip("Used for jumping the character")]
        [SerializeField] private Rigidbody2D _rigidBody;

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

                    transform.Translate(0, 0.1f, 0); // Raises the player off the ground so we don't trigger the check for if we're at ground level early
                    //_rigidBody.MovePosition(new Vector2(0, 0.1f)); 

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
            while ((float)Math.Round(transform.position.y, 3) > (float)Math.Round(_playerStandingYPosition, 3)) // Mathf has no rounding funcitons that round to floats, so the Math class was needed here
            {
                float x = transform.position.x;
                float y = Mathf.Sin(Time.time);

                Vector2 amountToMove = new Vector2(x, y);

                _rigidBody.MovePosition(amountToMove);

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