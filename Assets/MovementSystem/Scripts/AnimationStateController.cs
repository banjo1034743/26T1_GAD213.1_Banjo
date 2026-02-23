using UnityEngine;
using UnityEditor.Animations;

namespace GAD213.P1.MovementSystem
{
    public class AnimationStateController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        const int idleState = 0;

        const int walkingState = 1;

        [Header("Components")]

        [SerializeField] private Animator _playerAnimator;

        [SerializeField] private AnimationState _walkState;

        #endregion

        #region Methods

        public void ToggleIdleState()
        {
            // Resets the animator playback speed to default to ensure animations don't play in reverse.
            _playerAnimator.SetFloat("playbackSpeed", 1);

            _playerAnimator.SetInteger("currentAnimationState", idleState);
        }

        public void ToggleWalkingState(float directionWalking)
        {
            switch (directionWalking)
            {
                case > 0:
                    _playerAnimator.SetFloat("playbackSpeed", 1);
                    _playerAnimator.SetInteger("currentAnimationState", walkingState);
                    break;
                case < 0:
                    _playerAnimator.SetFloat("playbackSpeed", -1);
                    _playerAnimator.SetInteger("currentAnimationState", walkingState);
                    break;
            }
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