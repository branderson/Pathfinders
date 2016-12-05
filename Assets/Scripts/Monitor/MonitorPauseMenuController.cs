using Assets.Scripts.UI;
using Assets.Utility;
using Assets.VR;
using UnityEngine;

namespace Assets.Monitor
{
    public class MonitorPauseMenuController : CustomMonoBehaviour, IMenu
    {
        [SerializeField] private MenuPanelController _instructionsPanel;
        [SerializeField] private VRMenuController _vrMenu;
        private MenuPanelController _panel;

        private bool _instructionsOpen = false;

        public bool InstructionsOpen
        {
            get { return _instructionsOpen; }
        }

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

        public void OpenInstructionsMenu()
        {
            _instructionsOpen = true;
            _instructionsPanel.Display();
            _vrMenu.OpenInstructionMenu();
        }

        public void CloseInstructionsMenu()
        {
            _instructionsOpen = false;
            _instructionsPanel.Hide();
            _vrMenu.CloseInstructionMenu();
        }
    }
}