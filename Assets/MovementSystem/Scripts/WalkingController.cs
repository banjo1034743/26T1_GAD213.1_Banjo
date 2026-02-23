using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    [RequireComponent (typeof (Rigidbody2D))]
    public class WalkingController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float _walkingSpeed;

        [Header("Components")]

        [Tooltip("Used for moving the character")]
        [SerializeField] private Rigidbody2D _rigidBody;

        #endregion

        #region Methods

        public void Walk(Vector3 positionToMoveTo)
        {
            //Debug.Log(positionToMoveTo);
            _rigidBody.MovePosition(transform.position += positionToMoveTo * Time.fixedDeltaTime * _walkingSpeed);
        }

        // Called in Start()
        private void InitializeVariables()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
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