using UnityEngine;

namespace GAD213.P2.InteractionSystem
{
    public class TestDummyHitDetector : HitDetector
    {
        #region Variables

        [Header("Attacks To Recognise")]

        [SerializeField] private string _attackWeakLowTag;

        [SerializeField] private string _attackWeakHighTag;

        [SerializeField] private string _attackStrongLowTag;

        [SerializeField] private string _attackStrongHighTag;

        [Header("Scripts")]

        [SerializeField] private TestDummyAnimationController _animationController;

        #endregion

        #region Unity Methods

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag(_attackWeakLowTag))
            {
                Debug.Log("The player has hit me!");

                _animationController.PlayHitAnimation();
            }
            else if (other.transform.CompareTag(_attackWeakHighTag))
            {
                _animationController.PlayHitAnimation();
            }
            else if (other.transform.CompareTag(_attackStrongLowTag))
            {
                _animationController.PlayHitAnimation();
            }
            else if (other.transform.CompareTag(_attackStrongHighTag))
            {
                _animationController.PlayHitAnimation();
            }
        }

        #endregion
    }
}