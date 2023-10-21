using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class JumpState : AnimatedState
    {
        public override void StartContext(IPromiseSM sm)
        {
            animator.PlayAnim(EAnimState.A_Jump);
            sm.GetState<MoveState>().EnableMovementWithCache();
            
            RegisterAnimationEndOnce(EAnimState.A_Jump, sm.GoToState<MoveState>);
        }
    }
}