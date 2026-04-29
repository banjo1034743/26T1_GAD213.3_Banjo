using UnityEngine;
using UnityEngine.UI;

namespace GAD213.P2.InteractionSystem
{
    public class HealthManager : MonoBehaviour
    {
        #region Variables

        [Header("Components")]

        [SerializeField] private Slider _healthBar;

        #endregion

        #region Methods

        public void UpdateHealth(float value)
        {
            Debug.Log("Updating health");
            _healthBar.value += value;
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