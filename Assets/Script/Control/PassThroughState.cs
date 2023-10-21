using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class PassThroughState: AnimatedState
    {
        IRegistration _endReg;
        public override void StartContext(IPromiseSM sm, object param)
        {
            var p = (PassThroughStateParam) param;
            animator.PlayAnim(p.animName);
            
            //Caching _endReg for early exit when attack during passthrough
            _endReg = RegisterAnimationEndOnce(p.animName, () => sm.GoToState<MoveState>() );
        }
        
        public override void OnEndContext()
        {
            base.OnEndContext();
            _endReg.Unregister();
        }
    }
    
    public struct PassThroughStateParam
    {
        public string animName;
    }
}