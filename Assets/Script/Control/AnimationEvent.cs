using System;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    [RequireComponent(typeof(Animator))]
    public class AnimationEvent : MonoBehaviour
    {
        private Animator _animator;
        
        private event Action<string> onAnimationCallback;
        private event Action<int> onAnimationEnd;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDisable()
        {
            onAnimationCallback = null;
            onAnimationEnd = null;
        }

        public void RegisterAnimEvent(Action<string> action)
        {
            onAnimationCallback += action;
        }
        
        public void RegisterAnimationEnd(Action<int> action)
        {
            onAnimationEnd += action;
        }
        
        public void UnregisterAnimEvent(Action<string> action)
        {
            onAnimationCallback -= action;
        }

        //Used for animation event only
        public void OnAnimationEvent(string message)
        {
            onAnimationCallback?.Invoke(message);
        }
        
        public void OnCurrentAnimationEnd()
        {
            var currentHash = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            onAnimationEnd?.Invoke(currentHash);
        }
    }
}