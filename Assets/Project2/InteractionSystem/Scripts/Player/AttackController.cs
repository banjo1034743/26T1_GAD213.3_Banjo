using GAD213.P1.MovementSystem;
using GAD213.P3.ConflictSystem.SpecialAttacks;
using UnityEngine;

namespace GAD213.P2.InteractionSystem
{
    public class AttackController : MonoBehaviour
    {
        #region Variables

        [Header("Parameters")]

        [Space(5)]

        [SerializeField] private float _attackWeakLowDamage;

        [SerializeField] private float _attackWeakHighDamage;

        [SerializeField] private float _attackStrongLowDamage;

        [SerializeField] private float _attackStrongHighDamage;

        [Header("Special Attacks")]

        [Space(5)]

        //[SerializeField] private SpecialAttack1 _specialAttack1;

        [SerializeField] private SpecialAttack2 _specialAttack2;

        [SerializeField] private GameObject _harpoon;

        [Header("Scripts")]

        [Space(5)]

        [SerializeField] private FightingManager _fightingManager;

        [SerializeField] private FightingAnimationController _animationController;

        [SerializeField] private HealthManager _healthManager;

        #endregion

        #region Methods

        public void AttackWeakLow()
        {
            Debug.Log("We ahve used our Low Weak Attack!");
            
            _animationController.ToggleAttackWeakLowState();

            //performed = true;
        }

        public void AttackWeakHigh()
        {
            _animationController.ToggleAttackWeakHighState();
        }

        public void AttackStrongLow()
        {
            _animationController.ToggleAttackStrongLowState();
        }

        public void AttackStrongHigh()
        {
            _animationController.ToggleAttackStrongHighState();
        }

        public void SpecialAttack1()
        {

        }

        public void SpecialAttack2()
        {
            _animationController.ToggleSpecialAttack2State();
            _harpoon.SetActive(true);
        }

        public void DealDamage(string attackName)
        {
            switch (attackName)
            {
                case "Attack Weak Low":
                    Debug.Log("We're dealing damage for the Attack Weak Low attack");
                    _healthManager.UpdateTestDummyHealth(-_attackWeakLowDamage);
                    break;
                case "Attack Weak High":
                    _healthManager.UpdateTestDummyHealth(-_attackWeakHighDamage);
                    break;
                case "Attack Strong Low":
                    _healthManager.UpdateTestDummyHealth(-_attackStrongLowDamage);
                    break;
                case "Attack Strong High":
                    _healthManager.UpdateTestDummyHealth(-_attackStrongHighDamage);
                    break;
            }
        }

        private void InitialiseVariables()
        {
            _harpoon.SetActive(false);
        }

        //void Duration()
        //{
        //    if (performed)
        //    {
        //        if (attackWeakLowDuration > 0)
        //        {
        //            attackWeakLowDuration -= Time.deltaTime;

        //            if (attackWeakLowDuration <= 0)
        //            {
        //                _fightingManager.IsAttacking = false;

        //                SetAttackDurations();

        //                performed = false;
        //            }
        //        }
        //    }
        //}

        //private void SetAttackDurations()
        //{
        //    attackWeakLowDuration = _attackWeakLowAnimation.length * 2;
        //}

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InitialiseVariables();
        }

        // Update is called once per frame
        void Update()
        {
            //Duration();
        }

        #endregion
    }
}