using Assets.Managers;
using Assets.Utility;
using Assets.VR;
using UnityEngine;

namespace Assets.Monitor
{
    public class MonitorMenuController : CustomMonoBehaviour
    {
        private enum MenuState
        {
            None,
            Pause,
            Command
        }

        [SerializeField] private MonitorPlayerController _monitorPlayer;
        [SerializeField] private VRMenuController _vrMenu;
        [SerializeField] private MonitorPauseMenuController _pauseMenu;
        [SerializeField] private MonitorCommandMenuController _commandMenu;
        [SerializeField] private MenuState _state = MenuState.None;
        [SerializeField] private bool _canChangeState = true;

        private void Start()
        {
            switch (_state)
            {
                case MenuState.None:
                    CloseMenu();
                    break;
                case MenuState.Pause:
                    OpenPauseMenu();
                    break;
                case MenuState.Command:
                    OpenCommandMenu();
                    break;
            }
        }

        private void Update()
        {
            if (_canChangeState)
            {
                switch (_state)
                {
                    case MenuState.None:
                        if (Input.GetButtonDown("Pause"))
                        {
                            OpenPauseMenu();
                        }
                        else if (Input.GetButtonDown("ToggleCommand"))
                        {
                            OpenCommandMenu();
                        }
                        break;
                    case MenuState.Pause:
                        if (Input.GetButtonDown("Pause"))
                        {
                            // Only close instructions panel if open
                            if (_pauseMenu.InstructionsOpen)
                            {
                                _pauseMenu.CloseInstructionsMenu();
                            }
                            else
                            {
                                // Otherwise close menu
                                CloseMenu();
                            }
                        }
                        break;
                    case MenuState.Command:
                        if (Input.GetButtonDown("Pause"))
                        {
                            OpenPauseMenu();
                        }
                        else if (Input.GetButtonDown("ToggleCommand"))
                        {
                            CloseMenu();
                        }
                        break;
                }
            }
        }

        public void OpenCommandMenu()
        {
            if (_canChangeState)
            {
                _state = MenuState.Command;
                _monitorPlayer.AllowControl = false;
                _monitorPlayer.LockedControl = true;
                _pauseMenu.Close();
                _commandMenu.Open();
            }
        }

        public void OpenPauseMenu()
        {
            if (_canChangeState)
            {
                _state = MenuState.Pause;
                _vrMenu.OpenPauseMenu();
                _monitorPlayer.AllowControl = false;
                _monitorPlayer.LockedControl = true;
                _commandMenu.Close();
                _pauseMenu.Open();
                EventManager.Instance.TriggerEvent("Pause");
            }
        }

        public void CloseMenu()
        {
            if (_canChangeState)
            {
                if (_state == MenuState.Pause)
                {
                    EventManager.Instance.TriggerEvent("Unpause");
                }
                _state = MenuState.None;
                _vrMenu.CloseMenu();
                _monitorPlayer.LockedControl = false;
                _monitorPlayer.AllowControl = true;
                _commandMenu.Close();
                _pauseMenu.Close();
            }
        }
    }
}