using CofyUI;

namespace CofyDev.RpgLegend
{
    public class BattleUIPanel: UIPanel<BattleUIPanel>
    {
        public override void ShowPanel(bool enable)
        {
            gameObject.SetActive(enable);
            ControlUIPanel.instance.ShowPanel(enable);
        }
    }
}