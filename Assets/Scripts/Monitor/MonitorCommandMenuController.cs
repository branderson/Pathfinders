using Assets.Scripts.UI;
using Assets.Utility;

namespace Assets.Monitor
{
    public class MonitorCommandMenuController : CustomMonoBehaviour, IMenu
    {
        private MenuPanelController _panel;

        private void Awake()
        {
            _panel = GetComponentInChildren<MenuPanelController>();
        }

        public void Open()
        {
            _panel.gameObject.SetActive(true);
        }

        public void Close()
        {
            _panel.gameObject.SetActive(false);
        }
    }
}