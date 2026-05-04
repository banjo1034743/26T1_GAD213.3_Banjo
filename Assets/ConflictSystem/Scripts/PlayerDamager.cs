using GAD213.P2.InteractionSystem;
using GAD213.P3.ConflictSystem.Relevance;
using UnityEngine;

namespace GAD213.P3.ConflictSystem.TestDummy
{
    public class PlayerDamager : MonoBehaviour
    {
        #region Variables

        [Header("Variables")]

        [Space(5)]

        [SerializeField] private float _damageToPlayer;

        private Timer _playerHealthDecreaseTimer;

        [SerializeField] private float _playerHealthDecreaseTimeRate; // Applied to timer duration

        [Header("Scripts")]

        [Space(5)]

        [SerializeField] private HealthManager _healthManager;

        [SerializeField] private RelevanceManager _relevanceManager;

        #endregion

        #region Methods

        private void Initialise()
        {
            _playerHealthDecreaseTimer.Duration = _playerHealthDecreaseTimeRate;
        }

        private void DecreasePlayerHealthOvertime()
        {
            if (_playerHealthDecreaseTimer.HasExpired == true)
            {
                _healthManager.UpdatePlayerHealth(-_damageToPlayer);
                _relevanceManager.IncreaseRelevance();
                _playerHealthDecreaseTimer.Restart();
            }
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Initialise();
        }

        // Update is called once per frame
        void Update()
        {
            DecreasePlayerHealthOvertime();
        }

        #endregion
    }
}