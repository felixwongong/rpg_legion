using System;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    [RequireComponent(typeof(Animator))]
    public class AnimationEvent : MonoBehaviour
    {
        private event Action<string> onAnimationCallback;

        public void RegisterCallback(Action<string> action)
        {
            onAnimationCallback += action;
        }

        public void OnAnimationEvent(string message)
        {
            onAnimationCallback?.Invoke(message);
        }
    }
}