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

        private void CallIdle()
        {
            if (_inputManager.GetMoveValue().x <= 0 && _inputManager.GetMoveValue().y == 0)
            {
                _idleController.Idle();
            }
        }

        private void CallWalk()
        {

        }

        private void CallJump()
        {

        }

        private void CallCrouch()
        {

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
            CallIdle();
        }

        #endregion
    }
}