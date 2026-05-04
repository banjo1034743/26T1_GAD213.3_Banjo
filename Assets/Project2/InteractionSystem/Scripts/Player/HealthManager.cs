using GAD213.P3.ConflictSystem.GameManager;
using UnityEngine;
using UnityEngine.UI;

namespace GAD213.P2.InteractionSystem
{
    public class HealthManager : MonoBehaviour
    {
        #region Variables

        [Header("Components")]

        [Space(5)]

        [SerializeField] private Slider _healthBarTestDummy;

        [SerializeField] private Slider _healthBarPlayer;

        #endregion

        #region Methods

        public void UpdatePlayerHealth(float value)
        {
            Debug.Log("Updating player health");
            _healthBarPlayer.value += value;
        }

        public void UpdateTestDummyHealth(float value)
        {
            Debug.Log("Updating test dummy health");
            _healthBarTestDummy.value += value;
        }

        public void CheckForEnd() // Called by sliders each time its updated
        {
            if (_healthBarPlayer.value <= 0 || _healthBarTestDummy.value <= 0)
            {
                GameManager.instance.EndGame();
            }
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