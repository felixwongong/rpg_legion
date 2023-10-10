﻿using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class JumpState : AnimatedState
    {
        protected override void StartContext(IPromiseSM sm, Promise<string> promise)
        {
            animator.Play(EAnimState.Jump);
            
            promise.OnSucceed(_ =>
            {
                sm.GoToState<MoveState>();
            });
        }
    }
}