using UnityEngine;
using UnityEditor.Animations;

namespace GAD213.P1.MovementSystem
{
    public class AnimationStateController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        const int idleState = 0;

        [Header("Components")]

        [SerializeField] private Animator _playerAnimator;

        #endregion

        #region Methods

        public void ToggleIdleState()
        {
            _playerAnimator.SetInteger("currentAnimationState", idleState);
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