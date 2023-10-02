using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CofyDev.RpgLegend
{
    [RequireComponent(typeof(PlayerInput))]
    public class TopDownController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private Vector2 speed;

        private Vector3 _initScale;

        //STATE
        private Vector2 m_direction;

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
            if (m_direction != Vector2.zero)
            {
                _rb.velocity = m_direction * speed;
                HandleFlip();
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
        }

        private void HandleFlip()
        {
            if (m_direction.x != 0)
            {
                transform.localScale = new Vector3(Math.Sign(m_direction.x) * _initScale.x, _initScale.y, _initScale.z);
            }
        }

        private void OnMoveReceived(InputAction.CallbackContext context)
        {
            if (context.canceled) m_direction = Vector2.zero;
            else if (context.performed)
            {
                m_direction = context.ReadValue<Vector2>();
            }
        }
    }
}