using System;
using CofyEngine;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    public abstract class AnimatedState : MonoBehaviour, IPromiseState
    {
        [SerializeField] protected CofyAnimator animator;

        private IRegistration _eventReg;
        private IRegistration _endReg;
        
        protected virtual void Awake()
        {
            if (!animator) animator = GetComponentInChildren<CofyAnimator>();
        }

        private void OnDisable()
        {
            _eventReg = null;
            _endReg = null;
        }

        public abstract void StartContext(IPromiseSM sm);

        public virtual void OnEndContext(){}
        
        protected void RegisterAnimationEndOnce(string animName, Action callback)
        {
            _endReg = animator.eventHandler.onAnimationEnd.AddListenerOnce(ended =>
            {
                if (ended == animName)
                {
                    callback();
                }
            });
        }
        
        protected void RegisterAnimationEvent(Action<string> callback)
        {
            _eventReg = animator.eventHandler.onAnimationCallback.AddListener(callback);
        }
    }
}