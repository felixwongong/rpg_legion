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

        //State
        private Promise<bool> _animEndPromise = new();
        private Action<string> _eventAction;
        
        protected abstract string animName { get; }
        
        protected virtual void Awake()
        {
            if (!animator) animator = GetComponentInChildren<CofyAnimator>();
        }
        
        protected virtual void OnEnable()
        {
            animator.RegisterAnimationEnd(animName, () =>
            {
                _animEndPromise ??= new Promise<bool>();
                _animEndPromise?.Resolve(true);
                _animEndPromise?.Reset();
            });
        }

        public abstract void StartContext(IPromiseSM sm);

        public virtual void OnEndContext()
        {
            animator.UnregisterAnimationEvent(_eventAction);
        }
        
        protected void RegisterAnimationEndOnce(Action callback)
        {
            _animEndPromise.OnSucceed(_ => { callback.Invoke(); });
        }
        
        protected void RegisterAnimationEvent(Action<string> callback)
        {
            _eventAction = callback; 
            animator.RegisterAnimationEvent(_eventAction);
        }
    }
}