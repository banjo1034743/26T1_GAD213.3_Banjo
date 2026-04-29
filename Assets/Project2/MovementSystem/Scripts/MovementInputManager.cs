using GAD213.P2.InteractionSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GAD213.P1.MovementSystem
{
    public class MovementInputManager : MonoBehaviour
    {
        #region Variables

        [Header("<b>Input System</b>")]

        [Space(15)]

        [SerializeField] private PlayerInput _playerInput;

        [SerializeField] private InputActionAsset _inputActionAsset; // Need to initialise seperatley as will throw an error in OnEnable otherwise

        private InputActionMap _inputActionMap;

        // === INPUT ACTIONS ===

        private InputAction _inputActionMove;

        // === INPUT DEVICES ===

        private const int _usingGamepad = 0;

        private const int _usingKeyboard = 1;

        [Header("Scripts")]

        [Space(5)]

        [SerializeField] private FightingInputManager _fightingInputManager;

        #endregion

        #region Methods
        
        public Vector2 GetMoveValue()
        {
            //Debug.Log(_inputActionMove.ReadValue<Vector2>());

            // If we are using the controller, we return this
            return _inputActionMove.ReadValue<Vector2>();

            // otherwise, return values related to what buttons we have pressed on our keyboard
        }

        /// <summary>
        /// Used by FightingInputManager to read for special move inputs. Called in Update
        /// </summary>
        private void GetMoveInputs()
        {
            if (_inputActionMove.WasPerformedThisFrame() == true)
            {
                switch (_inputActionMove.ReadValue<Vector2>().x)
                {
                    case > 0:
                        _fightingInputManager.CheckSpecialAttack1Performed("MoveRight");
                        _fightingInputManager.CheckSpecialAttack2Performed("MoveRight");
                        break;
                    case < 0:
                        _fightingInputManager.CheckSpecialAttack1Performed("MoveLeft");
                        _fightingInputManager.CheckSpecialAttack2Performed("MoveLeft");
                        break;
                    default:
                        break;
                }
                switch (_inputActionMove.ReadValue<Vector2>().y)
                {
                    case > 0:
                        _fightingInputManager.CheckSpecialAttack1Performed("Jump");
                        _fightingInputManager.CheckSpecialAttack2Performed("Jump");
                        break;
                    case < 0:
                        _fightingInputManager.CheckSpecialAttack1Performed("Crouch");
                        _fightingInputManager.CheckSpecialAttack2Performed("Crouch");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Our other methods call this to check whether the player is using a keyboard or controller.
        /// We want to return one of the const int values to represent one of these options
        /// </summary>
        /// <returns></returns>
        public int ControlSchemeUsed() 
        {
            if (_playerInput.currentControlScheme == "Controller")
            {
                //Debug.Log("We are using the Gamepad!");
                return _usingGamepad;
            }
            else if (_playerInput.currentControlScheme == "Keyboard")
            {
                //Debug.Log("We are using the keyboard!");
                return _usingKeyboard;
            }
            else
            {
                return 1;
            }

        }

        private void InitializeInputActions()
        {
            _inputActionMap = _inputActionAsset.FindActionMap("MovementSystem");

            _inputActionMove = _inputActionMap.FindAction("Move");
        }

        #endregion

        #region Unity Methods

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InitializeInputActions();
        }

        private void Update()
        {
            GetMoveInputs();
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