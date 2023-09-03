using CofyEngine;
using CofyUI;

namespace CofyDev.RpgLegend
{
    public class BattleState: GameState
    {
        protected override string scene => "BattleScene";
        protected override IUIPanel uiPanel => BattleUIPanel.instance;
    }
}