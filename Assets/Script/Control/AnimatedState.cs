using System;
using CofyEngine;
using UnityEngine;
using UnityEngine.Serialization;

namespace CofyDev.RpgLegend
{
    public abstract class AnimatedState : MonoBehaviour, IPromiseState
    {
        [FormerlySerializedAs("_animator")] [SerializeField]
        protected CofyAnimator animator;

        private Promise<string> _animPromise = new ();
        
        protected virtual void Awake()
        {
            if (!animator) animator = GetComponentInChildren<CofyAnimator>();
        }
        
        protected virtual void OnEnable()
        {
            animator.RegisterAnimEnd(animName =>
            {
                _animPromise ??= new Promise<string>();
                _animPromise?.Resolve(animName);
                _animPromise?.Reset();
            });
        }

        public abstract void StartContext(IPromiseSM sm);
            
        public virtual void OnEndContext() { }
        
        protected void RegisterAnimationEndOnce(string animName, Action callback)
        {
            _animPromise.OnSucceed(inName =>
            {
                if (inName.Equals(animName)) callback?.Invoke();
            });
        }
    }
}