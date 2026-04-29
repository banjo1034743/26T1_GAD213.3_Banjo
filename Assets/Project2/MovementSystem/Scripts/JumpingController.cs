using System;
using System.Collections;
using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class JumpingController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        [SerializeField] private float _gravityScale;

        [SerializeField] private float _jumpHeight;

        [SerializeField] private float _horizontalJumpPower = 1;

        public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }

        [SerializeField] private bool _isJumping = false;

        [Header("Components")]

        [Tooltip("Used for jumping the character")]
        [SerializeField] private Rigidbody2D _rigidBody;

        [Header("Scripts")]

        [SerializeField] private MovementManager _characterController;

        [SerializeField] private MovementAnimationController _animationStateController;

        [SerializeField] private GroundChecker _groundChecker;

        #endregion

        #region Methods

        public void Jump(int jumpType, float horizontalInput)
        {
            // CODE SOURCE: (Game Dev Beginner, 2022, 6:26)
            _rigidBody.gravityScale = _gravityScale;
            float verticalJumpForce = Mathf.Sqrt(_jumpHeight * (Physics2D.gravity.y * _rigidBody.gravityScale) * -2) * _rigidBody.mass;
            _rigidBody.AddForce(Vector2.up * verticalJumpForce, ForceMode2D.Impulse);

            // Depending on the jump, we'll play a different animation
            switch (jumpType)
            {
                case 0:
                    _animationStateController.ToggleJumpVerticalState();
                    break;
                case 1:
                    _animationStateController.ToggleJumpHorizontalState();
                    _rigidBody.AddForce(new Vector2(horizontalInput * _horizontalJumpPower, 0), ForceMode2D.Force);
                    break;
            }

            _isJumping = true;
        }

        private void CheckIfLandedJump()
        {
            // When we're jumping, we'll begin checking if we've landed
            if (_isJumping)
            {
                if (_groundChecker.IsOnGround())
                {
                    _isJumping = false;
                    _animationStateController.ToggleIdleState();
                }
            }
        }

        #endregion

        #region Unity Methods

        // Update is called once per frame
        void Update()
        {
            CheckIfLandedJump();
        }

        #endregion
    }
}