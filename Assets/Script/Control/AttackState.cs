using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class AttackState: AnimatedState
    {
        protected override string animName => EAnimState.A_Attack;

        public override void StartContext(IPromiseSM sm)
        {
            sm.GetState<MoveState>().DisableInputWithCache();
            
            animator.PlayAnim(animName);                
            
            RegisterAnimationEndOnce(sm.GoToState<MoveState>);
        }
    }
}