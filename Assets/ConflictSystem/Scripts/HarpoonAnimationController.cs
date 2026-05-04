using UnityEngine;

namespace GAD213.P3.ConflictSystem.SpecialAttacks
{
    public class HarpoonAnimationController : MonoBehaviour
    {
        #region Variables

        [Header("Animation")]

        [Space(5)]

        [SerializeField] private Animator _harpoonAnimator;

        private const int _harpoonThrow = 1;

        private const int _harpoonReturn = 2;

        #endregion

        #region Methods

        public void ToggleHarpoonThrowAnimation()
        {
            Debug.Log("Toggled throw animation");

            _harpoonAnimator.SetInteger("currentAnimationState", _harpoonThrow);
        }

        public void ToggleHarpoonReturnAnimation()
        {
            Debug.Log("Toggled return animation");

            _harpoonAnimator.SetInteger("currentAnimationState", _harpoonReturn);
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