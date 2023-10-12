using System;
using CofyEngine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CofyDev.RpgLegend
{
    [RequireComponent(typeof(PlayerInput))]
    public class MoveState : AnimatedState
    {
        [Header("Reference")]
        [SerializeField] private Rigidbody2D _rb;

        [Header("Configurations")]
        [SerializeField] private Vector2 velocity_Max;
        [SerializeField] private float accelerationTime;
        [SerializeField] private float decelerationTime;

        private Vector3 _initScale;

        //STATE
        private Vector2 inputDirection;
        private Vector2 inputDirection_Cached;
        private Vector2 velocity_Current;
        [SerializeField] private bool enableMovement;

        protected override void Awake()
        {
            base.Awake();
            
            if(!_rb) _rb = GetComponent<Rigidbody2D>();
            _initScale = transform.localScale;
        }

        private void Update()
        {
            if(!enableMovement) return;
            
            if (inputDirection != Vector2.zero)
            {
                HandleFlip();
                
                velocity_Current = Vector2.SmoothDamp(velocity_Current, inputDirection * velocity_Max, ref velocity_Current, accelerationTime);
            }
            else
            {
                velocity_Current = Vector2.SmoothDamp(velocity_Current, Vector2.zero, ref velocity_Current, decelerationTime);
            }

            _rb.velocity = velocity_Current;
            animator.SetStateValue(EAnimState.RunState, velocity_Current.magnitude / velocity_Max.magnitude);
        }

        private void HandleFlip()
        {
            if (inputDirection.x != 0)
            {
                transform.localScale =
                    new Vector3(Math.Sign(inputDirection.x) * _initScale.x, _initScale.y, _initScale.z);
            }
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            if (context.canceled || !enableMovement)
            {
                inputDirection = inputDirection_Cached = Vector2.zero;
                return;
            }

            inputDirection = context.ReadValue<Vector2>();
        }

        protected override void StartContext(IPromiseSM sm, Promise<string> promise)
        {
            if (!enableMovement)
                animator.Play(EAnimState.RunState);
            
            enableMovement = true;
            inputDirection = inputDirection_Cached;
            Debug.Log($"applying cache {inputDirection_Cached.x}, {inputDirection_Cached.y}");
        }

        public override void OnEndContext()
        {
            base.OnEndContext();
            enableMovement = false;
            DisableInputWithCache();
        }

        public void DisableInputWithCache()
        {
            if(inputDirection != Vector2.zero) inputDirection_Cached = inputDirection; 
            inputDirection = Vector2.zero;
        }
    }
}