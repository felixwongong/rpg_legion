using System;
using System.Collections.Generic;
using CofyEngine;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    [DefaultExecutionOrder(-10)]
    public class CofyAnimator: MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private Animator _animator;

        [SerializeField] private AnimationEvent _event;
        
        Dictionary<string, int> _nameToHash = new ();
        
        private void Awake()
        {
            if(!_animator) _animator = GetComponentInChildren<Animator>();
            if (!_event) _event = GetComponentInChildren<AnimationEvent>();
        }

        private void Start()
        {
            _animator.runtimeAnimatorController.animationClips.ForEach(clip =>
            {
                _nameToHash.Add(clip.name, Animator.StringToHash(clip.name));
            });
            
            _animator.parameters.ForEach(param =>
            {
                _nameToHash.Add(param.name, Animator.StringToHash(param.name));
            });
        }

        public void RegisterAnimEnd(Action<string> callback)
        {
            _event.RegisterCallback(callback);
        }

        public void RegisterAnimationEnd(string animName, Action callback)
        {
            _event.RegisterAnimationEnd(hash =>
            {
                if(Animator.StringToHash(animName) == hash) callback();
            });
        }
        
        public void SetFloat01(string paramName, float value)
        {
            _animator.SetFloat(getHash(paramName), math.clamp(value, 0, 1));
        }

        public void SetTrigger(string paramName)
        {
            _animator.SetTrigger(getHash(paramName));
        }

        public void PlayAnim(string animName)
        {
            _animator.Play(getHash(animName));
        }
        
        public void PlayState(string stateName)
        {
            _animator.Play(stateName);
        }

        private int getHash(string animName)
        {
            if (!_nameToHash.TryGetValue(animName, out var hash))
            {
                throw new Exception(string.Format("No Animation Registered: {0}", animName));
            }

            return hash;
        }
    }
}