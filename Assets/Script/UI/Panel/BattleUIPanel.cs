using CofyUI;

namespace CofyDev.RpgLegend
{
    public class BattleUIPanel: UIInstance<BattleUIPanel>, IUIPanel
    {
        public void ShowPanel(bool enable)
        {
            gameObject.SetActive(enable);
            ControlUIPanel.instance.ShowPanel(enable);
        }
    }
}