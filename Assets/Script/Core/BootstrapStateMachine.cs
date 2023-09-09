using CofyEngine;
using UnityEngine;

public class BootstrapStateMachine : MonoBehaviour
{
    [SerializeField] private GameStateMachine _gsm;
    
    private StateMachine sm;

    private void Awake()
    {
        sm = new StateMachine();
        
        sm.RegisterState(new CMBootstrapUI());
        sm.RegisterState(new BootstrapUGS());
        sm.RegisterState(new TerminateState());

        sm.GoToNextState<BootstrapUI>();
    }
}
