using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class AttackState: AnimatedState
    {
        public override void StartContext(IPromiseSM sm)
        {
            sm.GetState<MoveState>().DisableInputWithCache();
            
            animator.Play(EAnimState.AttackState);                
            
            RegisterAnimationEndOnce(EAnimState.AttackState, sm.GoToState<MoveState>);
        }
    }
}