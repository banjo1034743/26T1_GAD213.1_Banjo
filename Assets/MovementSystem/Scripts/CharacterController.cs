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

        [Header("Scripts")]

        [SerializeField] private InputManager _inputManager;

        #endregion

        #region Methods

        // Called every frame in Update()
        private void CallIdle()
        {
            if (_inputManager.GetMoveValue().x <= 0 && _inputManager.GetMoveValue().y == 0)
            {
                _idleController.Idle();
            }
        }

        // Called in FixedUpdate(), as uses RB.MovePosition()
        private void CallWalk()
        {
            //Debug.Log(_inputManager.GetMoveValue());

            // If the player is moving the analog stick to the left or right without angling it upward, move

            if (_inputManager.GetMoveValue().x > 0 || _inputManager.GetMoveValue().x < 0 && _inputManager.GetMoveValue().y < 0.5f)
            {
                _walkingController.Walk(_inputManager.GetMoveValue());
            }
        }

        // Called every frame in Update()
        private void CallJump()
        {

        }

        // Called every frame in Update()
        private void CallCrouch()
        {

        }

        #endregion

        #region Unity Methods

        // Update is called once per frame
        void Update()
        {
            CallIdle();
        }

        private void FixedUpdate()
        {
            CallWalk();
        }

        #endregion
    }
}