using CofyUI;

namespace CofyDev.RpgLegend
{
    public class ControlUIPanel: UIInstance<ControlUIPanel>, IUIPanel
    {
        public void ShowPanel(bool enable) { gameObject.SetActive(enable); }
    }
}