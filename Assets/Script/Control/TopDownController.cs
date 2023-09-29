using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CofyDev.RpgLegendr
{
    public class TopDownController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput _input;

        private void Awake()
        {
            _input ??= GetComponent<PlayerInput>();
            if(!_input) FLog.LogException(new ArgumentNullException(nameof(_input)));
        }

        private void OnEnable()
        {
            _input.onActionTriggered += ReceiveInputAction;
        }

        private void OnDisable()
        {
            _input.onActionTriggered -= ReceiveInputAction;
        }

        private void ReceiveInputAction(InputAction.CallbackContext context)
        {
            FLog.Log("Input received", context);
        }
    }
}