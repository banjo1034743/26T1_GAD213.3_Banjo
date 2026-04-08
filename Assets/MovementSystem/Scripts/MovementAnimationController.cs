using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class MovementAnimationController : MonoBehaviour
    {
        #region Variables

        [Header("Data")]

        const int idleState = 0;

        const int walkingState = 1;

        const int crouchingState = 2;

        const int jumpVerticalState = 3;

        const int jumpHorizontalState = 4;

        [Header("Components")]

        [SerializeField] private Animator _playerAnimator;

        [SerializeField] private AnimationState _walkAnimationState;

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
            // This will be chanegd to look for the const int provided from InputManager
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

        public void ToggleCrouchState()
        {
            _playerAnimator.SetFloat("playbackSpeed", 1);

            _playerAnimator.SetInteger("currentAnimationState", crouchingState);
        }

        public void ToggleJumpVerticalState()
        {
            _playerAnimator.SetFloat("playbackSpeed", 1);

            _playerAnimator.SetInteger("currentAnimationState", jumpVerticalState);
        }

        public void ToggleJumpHorizontalState()
        {
            _playerAnimator.SetFloat("playbackSpeed", 1);

            _playerAnimator.SetInteger("currentAnimationState", jumpHorizontalState);
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