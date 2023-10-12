using CofyDev.RpgLegend;
using CofyEngine;
using CofyEngine.Util;
using UnityEngine.AddressableAssets;

public class BootstrapStateMachine: Instance<BootstrapStateMachine>
{
    private StateMachine sm;

    public BootstrapStateMachine()
    {
        sm = new StateMachine(true);

        sm.RegisterState(new CMBootstrapUI());
        sm.RegisterState(new BootstrapUGS());
        sm.RegisterState(new TerminateState(GameStateMachineImpl.instance));
    }

    public void Init()
    {
        Addressables.InitializeAsync(true).Future().OnCompleted(_ =>
        {
            sm.GoToState<CMBootstrapUI>();
        });
    }
}
