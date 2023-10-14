using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class JumpState : AnimatedState
    {
        protected override string animName => EAnimState.A_Jump;

        public override void StartContext(IPromiseSM sm)
        {
            animator.PlayAnim(animName);
            sm.GetState<MoveState>().EnableMovementWithCache();
            
            RegisterAnimationEndOnce(sm.GoToState<MoveState>);
        }
    }
}