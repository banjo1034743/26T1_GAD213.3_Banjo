using GAD213.P1.MovementSystem;
using GAD213.P2.InteractionSystem;
using UnityEngine;

namespace GAD213.P3.ConflictSystem.SpecialAttacks
{
    public class Harpoon : MonoBehaviour
    {
        #region Variables

        [Header("Harpoon Components")]

        [Space(5)]

        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private HarpoonAnimationController _harpoonAnimationController;

        [SerializeField] private GameObject _harpoonCollider;

        private Timer _harpoonReturnDelayTimer; // The gap in time after hitting enemy with harpoon before pulling them in
        public Timer HarpoonReturnDelayTimer { get {  return _harpoonReturnDelayTimer; } }

        [Header("Harpoon Parameters")]

        [Space(5)]

        [SerializeField] private float _movementSpeed = 1;

        [SerializeField] private bool _hasHit = false;
        public bool HasHit { get { return _hasHit; } set { _hasHit = value; } }

        [SerializeField] private float _harpoonReturnDelayAmount = 0.1f;

        private bool _returningHarpoon = false;
        public bool ReturningHarpoon { get { return _returningHarpoon; } set { _returningHarpoon = value; } }

        private Vector2 _originalHarpoonLocalPosition;

        [Header("Test Dummy")]

        [Space(5)]

        private GameObject _testDummy;
        public GameObject TestDummy { get { return _testDummy; } set { _testDummy = value; } }

        [Header("Scripts")]

        [Space(5)]

        [SerializeField] private MovementAnimationController _movementAnimationController;

        [SerializeField] private FightingManager _fightingManager;

        [SerializeField] private SoundPlayer _soundPlayer;

        #endregion

        #region Methods

        private void InitialiseHarpoon()
        {
            _originalHarpoonLocalPosition = transform.localPosition;

            _harpoonReturnDelayTimer.Duration = _harpoonReturnDelayAmount;

            _soundPlayer.PlaySFXClipAt("Harpoon Throw", transform.localPosition, 1);

            _harpoonAnimationController.ToggleHarpoonThrowAnimation();
        }

        private void MoveHarpoon()
        {
            if (_hasHit == false)
            {
                _rigidbody.MovePosition(new Vector2(_movementSpeed * Time.deltaTime, transform.position.y));
            }
        }

        //private void DetectHit(Collision2D collision)
        //{
        //    if (collision.gameObject.CompareTag("TestDummy") == true)
        //    {
        //        Debug.Log("Collided wiht test dummy");

        //        _hasHit = true;

        //        _harpoonReturnDelayTimer.Restart();

        //        _returningHarpoon = true;

        //        // WOuld add freeze code here for player but as this is for dummy dont need to bother.
        //    }
        //}

        public void RegisterHit(Collider2D testDummy)
        {
            Debug.Log("Collided wiht test dummy");

            _testDummy = testDummy.gameObject;

            _hasHit = true;

            _soundPlayer.PlaySFXClipAt("Harpoon Hit", transform.localPosition, 1);
            _soundPlayer.PlaySFXClipAt("Get Over Here", transform.localPosition, 1);

            _harpoonReturnDelayTimer.Restart();

            _returningHarpoon = true;

            // WOuld add freeze code here for player but as this is for dummy dont need to bother.
        }

        private void ReturnHarpoon()
        {
            if (_returningHarpoon == true && _harpoonReturnDelayTimer.HasExpired == true)
            {
                _harpoonAnimationController.ToggleHarpoonReturnAnimation();

                transform.localPosition = Vector2.MoveTowards(transform.localPosition, _originalHarpoonLocalPosition, _movementSpeed);

                _testDummy.transform.position = Vector2.MoveTowards(_testDummy.transform.position, _harpoonCollider.transform.position, _movementSpeed);

                if (transform.localPosition.x == _originalHarpoonLocalPosition.x)
                {
                    Debug.Log("Harpoon has reached OG Position");
                    _returningHarpoon = false;
                    _hasHit = false;
                    _fightingManager.IsAttacking = false; // Usually done in AnimationEVents for base attacks but needs to be done here for Harpoon
                    _movementAnimationController.ToggleIdleState();
                    
                    gameObject.SetActive(false);
                }
            }
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InitialiseHarpoon();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            MoveHarpoon();
        }

        private void Update()
        {
            ReturnHarpoon();
        }

        #endregion
    }
}