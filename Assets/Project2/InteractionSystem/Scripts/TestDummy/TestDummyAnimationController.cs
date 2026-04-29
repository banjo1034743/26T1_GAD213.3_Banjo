using UnityEngine;

namespace GAD213.P2.InteractionSystem
{
    public class TestDummyAnimationController : MonoBehaviour
    {
        #region Variables

        private const int idleState = 0;

        private const int hitState = 1;

        //private bool _hasBeenHit = false;

        [Header("Components")]

        [SerializeField] private Animator _animator;

        #endregion

        #region Methods

        public void PlayHitAnimation()
        {
            //_hasBeenHit = true;
            _animator.SetInteger("currentAnimationState", hitState);
        }

        public void PlayIdleAnimation()
        {
            _animator.SetInteger("currentAnimationState", idleState);
        }

        #endregion
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}