using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class WalkingController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float _walkingSpeed;

        [Header("Game Objects")]

        [SerializeField] private GameObject _playerColliderObject;

        [Header("Components")]

        [Tooltip("Used for moving the character")]
        [SerializeField] private Rigidbody2D _rigidBody;

        [Header("Scripts")]

        [SerializeField] private MovementAnimationController _animationStateController;

        [SerializeField] private GroundChecker _groundChecker;

        #endregion

        #region Methods

        public void Walk(Vector3 positionToMoveTo)
        {
            if (_groundChecker.IsOnGround())
            {
                //Debug.Log(positionToMoveTo);

                Vector3 movement = new Vector3(positionToMoveTo.x, 0, 0);

                _rigidBody.MovePosition(transform.position += movement * Time.fixedDeltaTime * _walkingSpeed);
                _animationStateController.ToggleWalkingState(movement.x);
            }
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        #endregion
    }
}