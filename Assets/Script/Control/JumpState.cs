using System;
using CofyEngine;
using UnityEngine;
using UnityEngine.Serialization;

namespace CofyDev.RpgLegend
{
    public class JumpState : MonoBehaviour, IPromiseState
    {
        [FormerlySerializedAs("_anim")]
        [Header("Reference")]
        [SerializeField]
        private CofyAnimator _animator;
        
        [Header("Configuration")]
        private float jumpMoveMultiplier = 0.5f;
        
        Promise<bool> _jumpPromise;

        private void Awake()
        {
            if (!_animator) _animator = GetComponentInChildren<CofyAnimator>();
        }

        private void OnEnable()
        {
            _animator.RegisterAnimEnd(CofyAnimator.Jump, animName =>
            {
                _jumpPromise?.Resolve(true);
            });
        }

        void IPromiseState.StartContext(IPromiseSM sm)
        {
            _jumpPromise = new Promise<bool>();
            _animator.Play(CofyAnimator.Jump);
            
            _jumpPromise.OnSucceed(_ =>
            {
                Debug.Log("Jump end");
                sm.GoToState<MoveState>();
            });
        }

        void IPromiseState.OnEndContext()
        {
        }
    }
}