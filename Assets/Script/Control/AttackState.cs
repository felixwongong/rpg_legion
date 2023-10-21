using CofyEngine;
using UnityEngine;

namespace CofyDev.RpgLegend
{
    public class AttackState: AnimatedState
    {
        private Attacker _attacker;

        private int comboIndex = 0;
        private string[] animSeq = { EAnimState.A_Attack1, EAnimState.A_Attack2 };

        private string curAnimName = string.Empty;

        protected override void Awake()
        {
            base.Awake();
            _attacker = GetComponent<PlayerController>().attacker;
        }

        public override void StartContext(IPromiseSM sm)
        {
            sm.GetState<MoveState>().DisableInputWithCache();

            curAnimName = animSeq[comboIndex];
            animator.PlayAnim(curAnimName);
            comboIndex = (comboIndex + 1) % animSeq.Length;
            
            RegisterAnimationEvent(message =>
            {
                Debug.Log(message);
            });
            
            RegisterAnimationEndOnce(curAnimName, sm.GoToState<MoveState>);
        }
    }
}