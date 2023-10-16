using CofyEngine;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    public class AttackState: AnimatedState
    {
        private Attacker _attacker;
        
        protected override string animName => EAnimState.A_Attack1;

        protected override void Awake()
        {
            base.Awake();
            _attacker = GetComponent<PlayerController>().attacker;
        }

        public override void StartContext(IPromiseSM sm)
        {
            sm.GetState<MoveState>().DisableInputWithCache();
            
            animator.PlayAnim(animName);
            
            RegisterAnimationEvent(message =>
            {
                Debug.Log(message);
            });
            
            RegisterAnimationEndOnce(() =>
            {
                sm.GoToState<MoveState>();
            });
        }
    }
}