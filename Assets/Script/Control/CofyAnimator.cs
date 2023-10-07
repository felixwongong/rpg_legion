using System;
using System.Collections.Generic;
using CofyEngine.Engine;
using Unity.Mathematics;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    public class CofyAnimator: MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private Animator _animator;
        [SerializeField] private List<Animation> registerEventAnimations;
        
        public static readonly string Run = "Run";
        public static readonly string RunState = "RunState";
        public static readonly string Jump = "Jump";

        private event Action<string> onAnimationEnd;

        private void Awake()
        {
            if (!_animator) _animator = GetComponentInChildren<Animator>();
        }

        private void OnDisable()
        {
            onAnimationEnd = null;
        }

        public void RegisterAnimEnd(string animName, Action<string> callback)
        {
            onAnimationEnd += callback;
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

        public void Play(string stateName)
        {
            _animator.Play(stateName);
            
            for (var i = 0; i < registerEventAnimations.Count; i++)
            {
                if (registerEventAnimations[i].name != stateName) continue;
                
                UnityFrameScheduler.instance.AddDelay(registerEventAnimations[i].clip.length, () =>
                {
                    onAnimationEnd?.Invoke(stateName);
                });
                break;
            }
        }
    }
}