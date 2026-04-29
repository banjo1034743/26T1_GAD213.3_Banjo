using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class IdleController : MonoBehaviour
    {
        #region Variables

        [Header("Scripts")]

        [SerializeField] private MovementAnimationController _animationStateController;

        #endregion

        #region Methods

        public void Idle()
        {
            _animationStateController.ToggleIdleState();
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