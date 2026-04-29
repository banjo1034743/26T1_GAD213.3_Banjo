using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class CrouchController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float _amountToShrinkColliderDown; //0.5

        // Getter and Setter
        public bool IsCrouching 
        {
            get
            {
                return _isCrouching;
            }
            set
            {
                _isCrouching = value;
            }
        } 

        [SerializeField] private bool _isCrouching = false; // Not starting game crouched

        [SerializeField] private float _amountToMoveColliderDown; // 0.4

        [Header("Components")]

        [SerializeField] private GameObject _collider;

        [Header("Scripts")]

        [SerializeField] private GroundChecker _groundChecker;

        [SerializeField] private MovementAnimationController _animationStateController;

        [SerializeField] private MovementManager _characterController;

        #endregion

        #region Methods

        public void Crouch()
        {
            if (_groundChecker.IsOnGround() && !_isCrouching)
            {
                //Debug.Log("We are crouching");

                // Scale the collider down to the needed size to mostly cover crouching sprite

                _collider.transform.localScale = new Vector2(1, _amountToShrinkColliderDown);

                // Move the scaled collider down to be over the sprite

                _collider.transform.localPosition = new Vector2(0, _amountToMoveColliderDown);

                _animationStateController.ToggleCrouchState();

                _isCrouching = true;
            }
        }

        public void Uncrouch()
        {
            if (_groundChecker.IsOnGround() && _isCrouching)
            {
                //Debug.Log("We are not crouching");

                _characterController.ResetCollider();

                _animationStateController.ToggleIdleState();

                _isCrouching = false;
            }
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _characterController.InitializeVariables();
        }

        #endregion
    }
}