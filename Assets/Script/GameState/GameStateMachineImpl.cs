using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class GameStateMachineImpl: GameStateMachine
    {
        private StateMachine sm;
        protected void Awake()
        {
            sm = new StateMachine();
            sm.RegisterState(new BattleState());
        }
        
        public override void Init()
        {
            sm.GoToNextState<BattleState>();
        }
    }
}