using CofyEngine;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    public class AttackState: AnimatedState
    {
        private Attacker _attacker;

        private int comboIndex = 0;
        private string[] animSeq = { EAnimState.A_Attack1, EAnimState.A_Attack2, EAnimState.A_Attack3 };

        private string curAnimName = string.Empty;

        protected override void Awake()
        {
            base.Awake();
            _attacker = GetComponent<PlayerController>().attacker;
        }

        public override void StartContext(IPromiseSM sm, object param)
        {
            sm.GetState<MoveState>().DisableInputWithCache();

            if (sm.previousState is not PassThroughState)
                comboIndex = 0;
            
            curAnimName = animSeq[comboIndex];
            animator.PlayAnim(curAnimName);
            comboIndex = (comboIndex + 1) % animSeq.Length;

            RegisterAnimationEndOnce(curAnimName, 
                () => sm.GoToState<PassThroughState>(new PassThroughStateParam() {animName = $"{curAnimName}_PT"}));
        }
    }
}