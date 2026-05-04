using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GAD213.P3.ConflictSystem.GameManager
{
    public class GameManager : MonoBehaviour
    {
        #region Static Declaration

        public static GameManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        #endregion

        #region Variables

        [Header("Variables")]

        [Space(5)]

        [SerializeField] private GameObject _menu;

        [SerializeField] private Button _retryButton;

        #endregion

        #region Methods

        public void EndGame()
        {
            _menu.SetActive(true);

            _retryButton.Select();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        private void Initialise()
        {
            _menu.SetActive(false); 
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialise();
        }

        #endregion
    }
}