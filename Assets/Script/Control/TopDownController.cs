using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace CofyDev.RpgLegend
{
    [RequireComponent(typeof(PlayerInput))]
    public class TopDownController : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Rigidbody2D _rb;

        [FormerlySerializedAs("maxSpeed")]
        [Header("Configurations")]
        [SerializeField] private Vector2 velocity_Max;
        [SerializeField] private float accelerationTime;
        [SerializeField] private float decelerationTime;

        private Vector3 _initScale;

        //STATE
        private Vector2 inputDirection;
        private Vector2 velocity_Current;

        private void Awake()
        {
            if(!_input) _input = GetComponent<PlayerInput>();
            if(!_rb) _rb = GetComponent<Rigidbody2D>();

            _initScale = transform.localScale;
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
                case "Move": OnMoveReceived(context); break;
            }
        }

        private void Update()
        {
            if (inputDirection != Vector2.zero)
            {
                HandleFlip();
                
                velocity_Current = Vector2.SmoothDamp(velocity_Current, inputDirection * velocity_Max, ref velocity_Current, accelerationTime);
            }
            else
            {
                velocity_Current = Vector2.SmoothDamp(velocity_Current, Vector2.zero, ref velocity_Current, decelerationTime);
            }

            Debug.Log($"{velocity_Current.x}, {velocity_Current.y}");

            _rb.velocity = velocity_Current;
        }

        private void HandleFlip()
        {
            if (inputDirection.x != 0)
            {
                transform.localScale =
                    new Vector3(Math.Sign(inputDirection.x) * _initScale.x, _initScale.y, _initScale.z);
            }
        }

        private void OnMoveReceived(InputAction.CallbackContext context)
        {
            if (context.canceled) inputDirection = Vector2.zero;
            else if (context.performed)
            {
                inputDirection = context.ReadValue<Vector2>();
            }
        }
    }
}