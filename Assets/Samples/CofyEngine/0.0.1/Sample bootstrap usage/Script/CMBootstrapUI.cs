using System.Collections.Generic;
using CofyDev.RpgLegend;
using CofyEngine;
using CofyEngine.Engine;
using CofyUI;
using UnityEngine;

public class CMBootstrapUI : BootstrapUI
{
    protected override Future<List<GameObject>> LoadAll()
    {
        var uiRoot = UIRoot.instance;

        List<Future<GameObject>> futures = new List<Future<GameObject>>();

        futures.Add(uiRoot.Bind<BattleUIPanel>(LoadUIAssetAsync("Panel/battle_panel")));
        futures.Add(uiRoot.Bind<ControlUIPanel>(LoadUIAssetAsync("Panel/control_panel")));

        if (futures.Count == 0)
        {
            Promise<GameObject> nullDelayPromise = new Promise<GameObject>();
            futures.Add(nullDelayPromise.future);
            
            UnityScheduler.instance.AddDelay(2000, () =>
            {
                nullDelayPromise.Resolve(null);
            });
        }
        
        return futures.Group();
    }
}