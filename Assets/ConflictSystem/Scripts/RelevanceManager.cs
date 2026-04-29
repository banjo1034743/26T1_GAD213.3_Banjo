using UnityEngine;
using UnityEngine.UI;

namespace GAD213.P3.ConflictSystem.Relevance
{
    public class RelevanceManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private bool _isDecreasingRelevance;
        public bool IsDecreasingRelevance { get { return _isDecreasingRelevance; } set { _isDecreasingRelevance = value; } }

        [SerializeField] private Slider _relevanceMeter;

        [SerializeField] private float _startingRelevanceValue = 0;

        [Tooltip("The amount of relevance that decreases when relevance goes down when not fighting")]
        [SerializeField] private float _relevanceDecreaseAmount;

        [SerializeField] private float _relevanceIncreaseAmount;

        [Tooltip("How long the gap is between relevance decreasing per interval")]
        [SerializeField] private float _relevanceDecreaseRate;

        [SerializeField] private float _relevanceIncreaseRate;

        [Tooltip("Time it takes before relevance decrease after not attacking other player")]
        [SerializeField] private float _timeBeforeBeforeStartRelevanceDecreaseDuration;

        //----

        private Timer _relevanceDecreaseGracePeriodTimer;

        private Timer _delayBetweenRelevanceDecrease;

        #endregion

        #region Methods

        public void DecreaseRelevance()
        {
            Debug.Log("Decreased Relevance in" + gameObject.name);
            _relevanceMeter.value -= _relevanceDecreaseAmount;
        }

        public void IncreaseRelevance()
        {
            Debug.Log("Increased Relevance in" + gameObject.name);
            _relevanceMeter.value += _relevanceIncreaseAmount;
        }

        public void SetRelevance(float amountToSetTo)
        {
            _relevanceMeter.value = amountToSetTo;
        }

        /// <summary>
        /// Called in AttackCollisionDetector after every hit and in start once for the
        /// start of the match
        /// </summary>
        public void StartRelevanceDecreaseGracePeriod()
        {
            Debug.Log("Began grace perioud before decreasing relevance in " + gameObject.name);

            _isDecreasingRelevance = false; // End of any decreasing that was happening after this is called again

            _relevanceDecreaseGracePeriodTimer.Restart(); // Start timer. Resets each time it is called when attacking. Only continues if not attacking for prolonged time
        }

        private void CheckForEndOfGracePeriod()
        {
            if (_relevanceDecreaseGracePeriodTimer.HasExpired == true) // When timer ends begin the process of decreasing relevance overtime
            {
                _isDecreasingRelevance = true;
            }
        }

        /// <summary>
        /// Called by StartRelevanceDecreaseOverTime. Will decrease relevance over time at an interval based on the value of the
        /// timer duration
        /// </summary>
        private void DecreaseRelevanceOverTime()
        {
            if (_isDecreasingRelevance == true) // This is set to false relevance is increased
            {
                if (_delayBetweenRelevanceDecrease.HasExpired == true)
                { 
                    DecreaseRelevance(); // Decreases relevance right after grace period before beginning to lose relevance ends

                    _delayBetweenRelevanceDecrease.Restart();
                }
            }
        }

        private void InitialiseRelevance()
        {
            _delayBetweenRelevanceDecrease.Duration = _relevanceDecreaseRate;
            _relevanceDecreaseGracePeriodTimer.Duration = _timeBeforeBeforeStartRelevanceDecreaseDuration;

            _isDecreasingRelevance = false;

            _relevanceMeter.value = _startingRelevanceValue;
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InitialiseRelevance();

            StartRelevanceDecreaseGracePeriod();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForEndOfGracePeriod(); // Need to check constantly to know when to start decreasing relevance

            DecreaseRelevanceOverTime(); // Called here as we're constantly checking if we're ready to decrease relevance passively overtime
        }

        #endregion
    }
}