using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class AttackState: AnimatedState
    {
        protected override void StartContext(IPromiseSM sm, Promise<string> promise)
        {
            sm.GetState<MoveState>().DisableInputWithCache();
            
            animator.Play(EAnimState.AttackState);                
            
            promise.OnSucceed(animName =>
            {
                sm.GoToState<MoveState>();
            });
        }
    }
}