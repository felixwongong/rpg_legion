using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CofyDev.RpgLegend
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReceiver: MonoBehaviour
    {
        //REF
        [SerializeField]
        private PlayerInput _input;

        //TODO: add smart Action for null check & auto dispose
        public event Action<Vector2> onVec2Updated;

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

            onVec2Updated = null;
        }
        
        private void ReceiveInputAction(InputAction.CallbackContext context)
        {
            switch (context.action.expectedControlType)
            {
               case "Vector2":
                   onVec2Updated?.Invoke(context.ReadValue<Vector2>());
                   break;
            }
        }
    }
}