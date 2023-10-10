using CofyEngine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CofyDev.RpgLegend
{
    public class TopDownController: MonoStateMachine
    {
        [Header("Reference")] 
        [SerializeField] private PlayerInput _input;

        private MoveState _moveState;
        protected override void Awake()
        {
            if(!_input) _input = GetComponent<PlayerInput>();
            
            base.Awake();
            RegisterState(GetComponent<MoveState>());
            RegisterState(GetComponent<JumpState>());
            RegisterState(GetComponent<AttackState>());
        }

        private void Start()
        {
            _moveState = GetState<MoveState>();
            GoToState<MoveState>();
        }
        
        private void OnEnable()
        {
            _input.onActionTriggered += OnInputActionTriggered;
        }

        private void OnDisable()
        {
            _input.onActionTriggered -= OnInputActionTriggered;
        }

        private void OnInputActionTriggered(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case "Move": _moveState.OnMoveInput(context); break;
                case "Jump": if(context.performed) GoToStateNoRepeat<JumpState>(); break;
                case "Attack": if (context.performed)  GoToStateNoRepeat<AttackState>(); break;
            }
        }
    }
}