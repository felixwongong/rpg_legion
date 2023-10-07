﻿using System;
using CofyEngine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CofyDev.RpgLegend
{
    [RequireComponent(typeof(PlayerInput))]
    public class MoveState : MonoBehaviour, IPromiseState
    {
        [Header("Reference")]
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private CofyAnimator _animator;

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

        private void Awake()
        {
            if(!_rb) _rb = GetComponent<Rigidbody2D>();
            if(!_animator) _animator = GetComponent<CofyAnimator>();

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
            _animator.SetStateValue(AnimState.RunState, velocity_Current.magnitude / velocity_Max.magnitude);
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
            if (!enableMovement) return;

            if (context.canceled)
                resetInputDirection();
            else if (context.performed)
            {
                inputDirection = context.ReadValue<Vector2>();
            } 
        }

        void IPromiseState.StartContext(IPromiseSM sm)
        {
            if (!enableMovement)
                _animator.Play(AnimState.RunState);
            
            enableMovement = true;
            
            if (inputDirection_Cached.notNullOrDefault())
            {
                inputDirection = inputDirection_Cached;
            }
        }

        void IPromiseState.OnEndContext()
        {
            enableMovement = false;
            resetInputDirection();
        }

        private void resetInputDirection()
        {
            inputDirection_Cached = inputDirection;
            inputDirection = Vector2.zero;
        }
    }
}