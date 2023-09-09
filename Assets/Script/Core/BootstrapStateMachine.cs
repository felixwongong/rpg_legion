using CofyDev.RpgLegend;
using CofyEngine;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BootstrapStateMachine : MonoBehaviour
{
    private StateMachine sm;

    private void Awake()
    {
        sm = new StateMachine();

        sm.RegisterState(new CMBootstrapUI());
        sm.RegisterState(new BootstrapUGS());
        sm.RegisterState(new TerminateState(GameStateMachineImpl.instance));

        Addressables.InitializeAsync(true).Future().OnCompleted(_ =>
        {
            sm.GoToNextState<CMBootstrapUI>();
        });
    }
}
