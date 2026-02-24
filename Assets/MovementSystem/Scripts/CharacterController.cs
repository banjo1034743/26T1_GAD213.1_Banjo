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

            if (_inputManager.GetMoveValue().x > 0 && _inputManager.GetMoveValue().y < _analogStickYValueAllowance || _inputManager.GetMoveValue().x < 0 && _inputManager.GetMoveValue().y < _analogStickYValueAllowance)
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
            if (_inputManager.GetMoveValue().y < 0 && _inputManager.GetMoveValue().x < _analogStickXValueAllowance || _inputManager.GetMoveValue().y < 0 && _inputManager.GetMoveValue().x > -_analogStickXValueAllowance)
            {
                _crouchController.Crouch();
            }
            else
            {
                _crouchController.Uncrouch();
            }
        }

        #endregion

        #region Unity Methods

        // Update is called once per frame
        void Update()
        {
            CallIdle();
            //CallCrouch();
        }

        private void FixedUpdate()
        {
            CallWalk();
        }

        #endregion
    }
}