using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] protected InputSystem_Actions _inputActions;
        private InputSystem_Actions.PlayerActions _playerActions;

        [SerializeField] private InputInfo _jumpInput;
        [SerializeField] private InputInfo _skillInput;


        #region Properties

        public Action<Vector3> OnMove { get; set; }
        public InputInfo JumpInput 
        {
            get { return _jumpInput; }
            private set { _jumpInput = value; } 
        }
        public InputInfo SkillInput
        {
            get { return _skillInput; }
            private set { _skillInput = value; }
        }

        #endregion

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            _playerActions = _inputActions.Player;
            _playerActions.Enable();
            _playerActions.Move.performed += MovePerformed;
            _playerActions.Move.canceled += MovePerformed;
            
            _playerActions.Jump.performed += _jumpInput.GetInput;
            _playerActions.Jump.canceled += _jumpInput.GetInput;

            _playerActions.Sprint.performed += _skillInput.GetInput;
            _playerActions.Sprint.canceled += _skillInput.GetInput;
        }

        private void MovePerformed(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            OnMove?.Invoke(new Vector3(moveInput.x, 0, moveInput.y));
        }
    }
}
