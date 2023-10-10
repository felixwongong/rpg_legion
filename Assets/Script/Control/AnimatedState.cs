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

        public void StartContext(IPromiseSM sm)
        {
            StartContext(sm, _animPromise);
        }

        protected abstract void StartContext(IPromiseSM sm, Promise<string> promise);
            

        public virtual void OnEndContext() { }
    }
}