using System;
using CofyEngine;
using Unity.Mathematics;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    public enum AnimState
    {
        Run, RunState, Jump
    }
    
    public class CofyAnimator: MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private Animator _animator;

        [SerializeField] private AnimationEvent _event;
        
        private void Awake()
        {
            if (!_animator) _animator = GetComponentInChildren<Animator>();
            if (!_event) _event = GetComponentInChildren<AnimationEvent>();
        }

        public void RegisterAnimEnd(Action<string> callback)
        {
            _event.RegisterCallback(callback);
        }
        
        public void SetStateValue(AnimState state, float value)
        {
            value = math.clamp(value, 0, 1);
            
            _animator.SetFloat(state.toStringCached(), 0.5f * value);
        }

        public void SetTrigger(AnimState trigger)
        {
            _animator.SetTrigger(trigger.toStringCached());
        }

        public void Play(AnimState state)
        {
            _animator.Play(state.toStringCached());
        }
    }
}