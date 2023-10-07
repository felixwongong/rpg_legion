using CofyEngine;

namespace CofyDev.RpgLegend
{
    public class GameStateMachineImpl: GameStateMachine
    {
        private static GameStateMachineImpl _instance;
        public static GameStateMachineImpl instance { get { return _instance ??= MonoUtils.createDDOL<GameStateMachineImpl>(); } }
        
        private StateMachine sm;
        
        private GameStateMachineImpl() 
        {
            sm = new StateMachine(true);
            sm.RegisterState(new BattleState());
        }
        
        public override void Init()
        {
            sm.GoToState<BattleState>();
        }
    }
}