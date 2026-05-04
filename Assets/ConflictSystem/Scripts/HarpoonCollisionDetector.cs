using GAD213.P2.InteractionSystem;
using UnityEngine;

namespace GAD213.P3.ConflictSystem.SpecialAttacks
{
    public class HarpoonCollisionDetector : AttackCollisionDetector
    {
        #region Variables

        [Header("Harpoon")]

        [Space(5)]

        [SerializeField] private Harpoon _harpoon;

        #endregion

        #region Methods

        private void DetectHit(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("TestDummy") == true)
            {
                _harpoon.RegisterHit(collision);
            }
        }

        #endregion

        #region Unity Methods

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            DetectHit(collision);
        }

        #endregion
    }
}