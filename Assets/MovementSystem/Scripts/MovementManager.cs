using GAD213.P2.InteractionSystem;
using UnityEngine;

namespace GAD213.P1.MovementSystem
{
    public class MovementManager : MonoBehaviour
    {
        #region Variables

        [Header("Controllers")]

        [SerializeField] private IdleController _idleController;

        [SerializeField] private WalkingController _walkingController;

        [SerializeField] private JumpingController _jumpingController;

        const int _jumpingVertically = 0;

        const int _jumpingHorizontally = 1;

        [SerializeField] private CrouchController _crouchController;

        [Header("Parameters")]

        [Tooltip("We don't want the player to move back and forth if the analog stick is angled more than going in the right or left direction. At the same time, we dont want to make it diffuclt to mvoe forward by making the input too closed off. This should be set to a sweet spot.")]
        [SerializeField] private float _analogStickYValueAllowance;

        [Tooltip("We don't want the player to move back and forth if the analog stick is angled left or right more than going down. At the same time, we dont want to make it diffuclt to crouch by making the input too closed off. This should be set to a sweet spot.")]
        [SerializeField] private float _analogStickXValueAllowance;

        //public bool IsActive { get { return _isActive; } set { _isActive = value; } }

        //[SerializeField] private bool _isActive;

        [Header("Collider Variables")]

        [SerializeField] private Vector2 _originalColliderScale;

        [SerializeField] private Vector2 _originalColliderPosition;

        [SerializeField] private GameObject _collider;

        [Header("Scripts")]

        [SerializeField] private MovementInputManager _inputManagerMovement;

        // Ideally we want to less intangle the Interaction System with the Movement System to make it
        // more encapsulated. If time allows it, lets refactor this
        [SerializeField] private FightingManager _fightingManager;

        #endregion

        #region Methods

        // Called every frame in Update()
        private void CallIdle()
        {
            // _inputManager.GetMoveValue().x == 0 && _inputManager.GetMoveValue().y == 0 && _jumpingController.IsJumping == false
            if (_inputManagerMovement.GetMoveValue().x == 0 && _inputManagerMovement.GetMoveValue().y == 0 && _jumpingController.IsJumping == false && _fightingManager.IsAttacking == false) // Dont want to switch to idle mid jump anim
            {
                Debug.Log("We're not active atm, so we're calling Idle");
                _idleController.Idle();
            }
        }

        // Called in FixedUpdate(), as uses RB.MovePosition()
        private void CallWalk()
        {
            if (_crouchController.IsCrouching == false && _jumpingController.IsJumping == false && _fightingManager.IsAttacking == false)
            {
                Debug.Log(_inputManagerMovement.GetMoveValue());

                // If the player is moving the analog stick to the left or right without angling it upward, move

                if (_inputManagerMovement.GetMoveValue().x > 0 && _inputManagerMovement.GetMoveValue().y < _analogStickYValueAllowance || _inputManagerMovement.GetMoveValue().x < 0 && _inputManagerMovement.GetMoveValue().y < _analogStickYValueAllowance)
                {
                    //_fightingManager.IsAttacking = false;
                    _walkingController.Walk(_inputManagerMovement.GetMoveValue());
                }
            }
        }

        // Called every frame in Update()
        private void CallJump()
        {
            // If the left analog stick is flicked up and not angled in the left or right too much, call vertical jump
            if (_fightingManager.IsAttacking == false)
            {
                if (HasDoneVerticalJumpInput() == true)
                {
                    if (_jumpingController.IsJumping == false)
                    {
                        _fightingManager.IsAttacking = false;
                        _jumpingController.Jump(_jumpingVertically, 0f);
                    }
                }
                // if we have flicked the analog stick to the upper right or left corners, call horizontal jump
                else if (HasDoneHorizontalJumpInput() == true)
                {
                    if (_jumpingController.IsJumping == false)
                    {
                        _fightingManager.IsAttacking = false;
                        _jumpingController.Jump(_jumpingHorizontally, _inputManagerMovement.GetMoveValue().x);
                    }
                }
            }
        }

        // Called every frame in Update()
        private void CallCrouch()
        {
            if (_fightingManager.IsAttacking == false)
            {
                if (_jumpingController.IsJumping == false)
                {
                    // If the left analog stick is flicked down, and not angled in any direction too far, crouch.
                    if (_inputManagerMovement.GetMoveValue().y < -0.9f && _inputManagerMovement.GetMoveValue().x < _analogStickXValueAllowance || _inputManagerMovement.GetMoveValue().y < -0.9f && _inputManagerMovement.GetMoveValue().x > -_analogStickXValueAllowance)
                    {
                        _fightingManager.IsAttacking = false;
                        _crouchController.Crouch();
                    }
                    // Otherwise, remain the same.
                    else
                    {
                        _crouchController.Uncrouch();
                    }
                }
            }
        }

        //private void DetermineIfActive()
        //{
        //    if (_inputManagerMovement.GetMoveValue().x == 0 && _inputManagerMovement.GetMoveValue().y == 0 && _jumpingController.IsJumping == false && _fightingManager.IsAttacking == false)
        //    {
        //        Debug.Log("We are not active");
        //        _isActive = false;
        //    }
        //    else if (_fightingManager.IsAttacking == true)
        //    {
        //        _isActive = true;
        //    }
        //    else
        //    {
        //        Debug.Log("We are currently active");
        //        _isActive = true;
        //    }
        //}

        private bool HasDoneVerticalJumpInput()
        {
            switch (_inputManagerMovement.ControlSchemeUsed())
            {
                case 0:
                    if (_inputManagerMovement.GetMoveValue().y > 0.9f && _inputManagerMovement.GetMoveValue().x < _analogStickXValueAllowance || _inputManagerMovement.GetMoveValue().y > 0.9f && _inputManagerMovement.GetMoveValue().x > -_analogStickXValueAllowance)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 1:
                    if (_inputManagerMovement.GetMoveValue().y > 0.9f && _inputManagerMovement.GetMoveValue().x == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    Debug.Log("Vertical jump failed wtf");
                    return false;
            }

        }

        private bool HasDoneHorizontalJumpInput()
        {
            switch (_inputManagerMovement.ControlSchemeUsed())
            {
                case 0:
                    // If the input stick is flicked diagonally in the upper right corner, we have inputted the command to jump forward
                    if (_inputManagerMovement.GetMoveValue().x >= 0.6f && _inputManagerMovement.GetMoveValue().x <= 0.9f && _inputManagerMovement.GetMoveValue().y >= 0.5f && _inputManagerMovement.GetMoveValue().y <= 0.7f)
                    {
                        return true;
                    }
                    // If the input stick is flicked diagonally in the upper left corner, we have inputted the command to jump backward
                    else if (_inputManagerMovement.GetMoveValue().x >= -0.9f && _inputManagerMovement.GetMoveValue().x <= -0.7f && _inputManagerMovement.GetMoveValue().y >= 0.4f && _inputManagerMovement.GetMoveValue().x <= 0.6f)
                    {
                        return true;
                    }
                    // Otherwise, we have not inputted any jumping command
                    else
                    {
                        return false;
                    }
                case 1:
                    // If the input stick is flicked diagonally in the upper right corner, we have inputted the command to jump forward
                    if (_inputManagerMovement.GetMoveValue().x > 0.7f && _inputManagerMovement.GetMoveValue().x < 0.72f && _inputManagerMovement.GetMoveValue().y > 0.7f && _inputManagerMovement.GetMoveValue().y < 0.72f)
                    {
                        return true;
                    }
                    // If the input stick is flicked diagonally in the upper left corner, we have inputted the command to jump backward
                    else if (_inputManagerMovement.GetMoveValue().x < -0.7f && _inputManagerMovement.GetMoveValue().x > -0.72f && _inputManagerMovement.GetMoveValue().y > 0.7f && _inputManagerMovement.GetMoveValue().y < 0.72f)
                    {
                        return true;
                    }
                    // Otherwise, we have not inputted any jumping command
                    else
                    {
                        return false;
                    }
                default:
                    Debug.Log("Horizontal jump failed wtf");
                    return false;
            }
        }

        public void InitializeVariables()
        {
            _originalColliderScale = _collider.transform.localScale;

            _originalColliderPosition = _collider.transform.localPosition;

            //_isActive = false;
        }

        public void ResetCollider()
        {
            _collider.transform.localScale = _originalColliderScale;

            _collider.transform.localPosition = _originalColliderPosition;
        }

        #endregion

        #region Unity Methods

        // Update is called once per frame
        void Update()
        {
            CallIdle();
            CallCrouch();

            //DetermineIfActive();
        }

        private void FixedUpdate()
        {
            CallWalk();
            CallJump();
        }

        private void Start()
        {
            InitializeVariables();
        }

        #endregion
    }
}