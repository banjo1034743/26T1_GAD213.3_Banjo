using GAD213.P3.ConflictSystem.Relevance;
using UnityEngine;

namespace GAD213.P2.InteractionSystem
{
    public class AttackCollisionDetector : MonoBehaviour
    {
        #region Variables

        [Header("Attack Names")]

        [Space(5)]

        [Tooltip("Set this to the name in the inspector")]
        [SerializeField] private string _attackName;

        [Header("Tags to read for")]

        [Space(5)]

        [Tooltip("Set this to the current tag of dummy in the inspector")]
        [SerializeField] private string _testDummyTag;

        [Header("Scripts")]

        [Space(5)]

        [Tooltip("Initialise in the inspector")]
        [SerializeField] private AttackController _attackController;

        [Tooltip("Initialise in the inspector")]
        [SerializeField] private SoundPlayer _soundPlayer;

        [Tooltip("Initialise in the inspector")]
        [SerializeField] private RelevanceManager _relevanceManager;

        //----

        // Initialised in first attack with the test dummy, this reference used from there
        private RelevanceManager _testDummyRelevance;

        #endregion

        #region Methods

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag(_testDummyTag) == true)
            {
                Debug.Log("We struck the dummy");
                _attackController.DealDamage(_attackName);
                _soundPlayer.PlaySFXClipAt(_attackName, transform.position, 1f);

                _relevanceManager.IncreaseRelevance();
                _relevanceManager.StartRelevanceDecreaseGracePeriod();

                if (_testDummyRelevance == null)
                {
                    RelevanceManager testDummyRelevance = collision.gameObject.GetComponent<RelevanceManager>();
                    _testDummyRelevance = testDummyRelevance;
                    _testDummyRelevance.DecreaseRelevance();
                }
                else
                {
                    _testDummyRelevance.DecreaseRelevance();
                }
            }
        }

        #endregion
    }
}