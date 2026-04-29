using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class GroundChecker : MonoBehaviour
    {
        #region Variables

        [SerializeField] private bool _isOnGround;

        #endregion

        #region Methods

        public bool IsOnGround()
        {
            // Value returned determined by OnCollisionEnter and Ext methods checking if collidng with ground
            return _isOnGround;
        }

        #endregion

        #region Unity Methods

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isOnGround = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isOnGround = false;
            }
        }

        #endregion
    }
}