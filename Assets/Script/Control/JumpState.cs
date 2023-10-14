using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class JumpState : AnimatedState
    {
        public override void StartContext(IPromiseSM sm)
        {
            animator.Play(EAnimState.Jump);
            sm.GetState<MoveState>().EnableMovementWithCache();
            
            RegisterAnimationEndOnce(EAnimState.Jump, sm.GoToState<MoveState>);
        }
    }
}