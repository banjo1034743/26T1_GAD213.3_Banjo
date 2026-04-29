using UnityEngine;
using UnityEngine.InputSystem;

namespace GAD213.P2.InteractionSystem
{
    public class FightingInputManager : MonoBehaviour
    {
        #region Variables

        [Header("<b>Input System</b>")]

        [Space(15)]

        [SerializeField] private PlayerInput _playerInput;

        [SerializeField] private InputActionAsset _inputActionAsset; // Need to initialise seperatley as will throw an error in OnEnable otherwise

        private InputActionMap _inputActionMap;

        // === INPUT ACTIONS ===

        private InputAction _inputActionMove;

        private InputAction _inputActionAttackWeakLow;

        private InputAction _inputActionAttackWeakHigh;

        private InputAction _inputActionAttackStrongLow;

        private InputAction _inputActionAttackStrongHigh;

        // === INPUT DEVICES ===

        private const int _usingGamepad = 0;

        private const int _usingKeyboard = 1;

        // === SPECIAL ATTACKS ===

        private int _completedInputsSpecialAttack1 = 0;

        private int _completedInputsSpecialAttack2 = 0;

        private int _inputsNeededSpecialAttack1 = 2;

        private int _inputsNeededSpecialAttack2 = 3;

        [SerializeField] private float _timeBetweenSpecialAttackInputs;

        private Timer _timeBetweenSpecialAttack1InputsTimer;

        private Timer _timeBetweenSpecialAttack2InputsTimer;

        [Tooltip("Inputs: MoveRight, MoveLeft, Jump, Crouch, AttackWeakLow, AttackWeakHigh, AttackStrongLow, AttackStrongHigh")]
        // MoveRight, MoveLeft
        [SerializeField] private string[] _specialAttack1Inputs;

        [Tooltip("Inputs: MoveRight, MoveLeft, Jump, Crouch, AttackWeakLow, AttackWeakHigh, AttackStrongLow, AttackStrongHigh")]
        // Crouch, MoveRight, Y
        [SerializeField] private string[] _specialAttack2Inputs;

        #endregion

        #region Methods

        /// <summary>
        /// Called in FightingManager, which determines whether AttackController runs functionality
        /// for attack
        /// </summary>
        /// <returns></returns>
        public bool AttackWeakLowPerformed()
        {
            switch (_inputActionAttackWeakLow.WasPerformedThisFrame())
            {
                case true:
                    CheckSpecialAttack1Performed("AttackWeakLow");
                    CheckSpecialAttack2Performed("AttackWeakLow");
                    return true;
                case false:
                    return false;
            }
        }

        /// <summary>
        /// Called in FightingManager, which determines whether AttackController runs functionality
        /// for attack
        /// </summary>
        /// <returns></returns>
        public bool AttackWeakHighPerformed()
        {
            switch (_inputActionAttackWeakHigh.WasPerformedThisFrame())
            {
                case true:
                    CheckSpecialAttack1Performed("AttackWeakHigh");
                    CheckSpecialAttack2Performed("AttackWeakHigh");
                    return true;
                case false:
                    return false;
            }
        }

        public bool AttackStrongLowPerformed()
        {
            switch (_inputActionAttackStrongLow.WasPerformedThisFrame())
            {
                case true:
                    CheckSpecialAttack1Performed("AttackStrongLow");
                    CheckSpecialAttack2Performed("AttackStrongLow");
                    return true;
                case false:
                    return false;
            }
        }

        public bool AttackStrongHighPerformed()
        {
            switch (_inputActionAttackStrongHigh.WasPerformedThisFrame())
            {
                case true:
                    CheckSpecialAttack1Performed("AttackStrongHigh");
                    CheckSpecialAttack2Performed("AttackStrongHigh");
                    return true;
                case false:
                    return false;
            }
        }

        public void CheckSpecialAttack1Performed(string inputName)
        {
            if (_timeBetweenSpecialAttack1InputsTimer.HasExpired == true)
            {
                Debug.Log("Special Move 1 Timeframe has started");
                RegisterSpecialAttackInput(inputName, _specialAttack1Inputs[_completedInputsSpecialAttack1], "SpecialAttack1");
                _timeBetweenSpecialAttack1InputsTimer.Restart();
                return;
            }
            else if (_timeBetweenSpecialAttack1InputsTimer.HasExpired == false)
            {
                RegisterSpecialAttackInput(inputName, _specialAttack1Inputs[_completedInputsSpecialAttack1], "SpecialAttack1");

                if (_completedInputsSpecialAttack1 == (_inputsNeededSpecialAttack1 - 1))
                {
                    // Do special attack
                    Debug.Log("Scorpion teleported behind enemy!"); // If I can't get the harpoon working soon then skip this
                    _completedInputsSpecialAttack1 = 0;
                }
            }
        }

        public void CheckSpecialAttack2Performed(string inputName)
        {
            if (_timeBetweenSpecialAttack2InputsTimer.HasExpired == true)
            {
                Debug.Log("Special Move 2 Timeframe has started");
                RegisterSpecialAttackInput(inputName, _specialAttack2Inputs[_completedInputsSpecialAttack2], "SpecialAttack2");
                _timeBetweenSpecialAttack2InputsTimer.Restart();
                return;
            }
            else if (_timeBetweenSpecialAttack2InputsTimer.HasExpired == false)
            {
                RegisterSpecialAttackInput(inputName, _specialAttack2Inputs[_completedInputsSpecialAttack2], "SpecialAttack2");

                if (_completedInputsSpecialAttack2 == (_inputsNeededSpecialAttack2 - 1))
                {
                    // Do special attack
                    Debug.Log("Scorpion used his harpoon!");
                    _completedInputsSpecialAttack2 = 0;
                }
            }
        }

        /// <summary>
        /// Compares the string with the inputs name to the string of the name of the wanted
        /// input in the special attack to see if the inputs are being done in the correct
        /// order. Increments value representing how many inputs of special atttack completed
        /// when true.
        /// </summary>
        /// <param name="providedInput"></param>
        /// <param name="wantedInput"></param>
        private void RegisterSpecialAttackInput(string providedInput, string wantedInput, string specialAttack)
        {
            if (providedInput.Equals(wantedInput))
            {
                Debug.Log("Did the correct input!");
                switch (specialAttack)
                {
                    case "SpecialAttack1":
                        if (_completedInputsSpecialAttack1 < _specialAttack1Inputs.Length - 1) // Dont want to crash game, need this
                        {
                            _completedInputsSpecialAttack1++;
                        }
                        break;
                    case "SpecialAttack2":
                        if (_completedInputsSpecialAttack2 < _specialAttack2Inputs.Length - 1)
                        {
                            _completedInputsSpecialAttack2++;
                        }
                        break;
                }
            }
        }

        private void CheckForEndOfTimeframe()
        {
            if (_timeBetweenSpecialAttack1InputsTimer.HasExpired == true)
            {
                _completedInputsSpecialAttack1 = 0;
            }
            if (_timeBetweenSpecialAttack2InputsTimer.HasExpired == true)
            {
                _completedInputsSpecialAttack2 = 0;
            }
        }

        private void InitializeInputActions()
        {
            _inputActionMap = _inputActionAsset.FindActionMap("Fighting");

            _inputActionMove = _inputActionMap.FindAction("Move");

            _inputActionAttackWeakLow = _inputActionMap.FindAction("Attack Weak Low");

            _inputActionAttackWeakHigh = _inputActionMap.FindAction("Attack Weak High");

            _inputActionAttackStrongLow = _inputActionMap.FindAction("Attack Strong Low");

            _inputActionAttackStrongHigh = _inputActionMap.FindAction("Attack Strong High");
        }

        private void InitializeVariables()
        {
            _timeBetweenSpecialAttack1InputsTimer.Duration = _timeBetweenSpecialAttackInputs;
            _timeBetweenSpecialAttack2InputsTimer.Duration = _timeBetweenSpecialAttackInputs;
        }

        #endregion

        #region Unity Methods

        void Start()
        {
            InitializeInputActions();
            InitializeVariables();
        }

        private void OnEnable()
        {
            _inputActionAsset.Enable();
        }

        private void OnDisable()
        {
            _inputActionAsset.Disable();
        }

        #endregion
    }
}