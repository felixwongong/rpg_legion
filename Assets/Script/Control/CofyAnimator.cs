using System;
using Unity.Mathematics;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    [DefaultExecutionOrder(-10)]
    public class CofyAnimator: MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private Animator _animator;

        [SerializeField] private AnimationEvent _event;
        
        private void Awake()
        {
            if(!_animator) _animator = GetComponentInChildren<Animator>();
            if (!_event) _event = GetComponentInChildren<AnimationEvent>();
        }

        public void RegisterAnimEnd(Action<string> callback)
        {
            _event.RegisterCallback(callback);
        }
        
        public void SetStateValue(string state, float value)
        {
            value = math.clamp(value, 0, 1);
            
            _animator.SetFloat(state, 0.5f * value);
        }

        public void SetTrigger(string trigger)
        {
            _animator.SetTrigger(trigger);
        }

        public void Play(string state)
        {
            _animator.Play(state);
        }
    }
}