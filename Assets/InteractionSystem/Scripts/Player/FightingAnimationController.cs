using UnityEngine;

namespace GAD213.P2.InteractionSystem
{
    public class FightingAnimationController : MonoBehaviour
    {
        #region Variables

        //public bool _isPlayingAnim = false;

        [Header("Components")]

        [SerializeField] private Animator _playerAnimator;

        [SerializeField] private AnimationClip _animationClip;

        // === ANIMATION STATES ===

        // Our const int values start at 5 as our MovementSystem animation uses these in the Animator
        // that begin from 0 and end at 4. Starting at 0 here would cause a mixup in the Animator when
        // it is checking which state it should transition to

        private const int _attackWeakLowState = 5;

        private const int _attackWeakHighState = 6;

        private const int _attackStrongLowState = 7;

        private const int _attackStrongHighState = 8;

        [Header("Scripts")]

        [Space(10)]

        [SerializeField] private FightingManager _fightingManager;

        #endregion

        #region Methods

        // TODO: TRY ADDING A BOOL SPECIFICALLY FOR CHECKING IF ANIM COMPLETED RATHER THAN CHECKING FOR ISFIGHTING

        public void ToggleAttackWeakLowState()
        {
            Debug.Log("We're setting the integer in the animation controller to 5");
            _playerAnimator.SetInteger("currentAnimationState", _attackWeakLowState);
        }

        public void ToggleAttackWeakHighState()
        {
            _playerAnimator.SetInteger("currentAnimationState", _attackWeakHighState);
        }

        public void ToggleAttackStrongLowState()
        {
            _playerAnimator.SetInteger("currentAnimationState", _attackStrongLowState);
        }

        public void ToggleAttackStrongHighState()
        {
            _playerAnimator.SetInteger("currentAnimationState", _attackStrongHighState);
        }

        public void EndAttackStateAnimation()
        {
            _fightingManager.IsAttacking = false;
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