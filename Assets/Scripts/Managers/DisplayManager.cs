using System;
using Assets.Monitor;
using Assets.Utility;
using Assets.VR;
using UnityEngine;

namespace Assets.Managers
{
    public enum DisplayConfiguration
    {
        VR,
        DualMonitor,
        SingleMonitorSplit,
        SingleMonitorToggle
    }

    public enum InputConfiguration
    {
        Gamepad,
        NoGamepad
    }

    /// <summary>
    /// Manages display and input configuration for development
    /// </summary>
    public class DisplayManager : Singleton<DisplayManager>
    {
        private static readonly Rect VRRect = new Rect(.5f, 0, .5f, 1);
        private static readonly Rect MonitorRect = new Rect(0, 0, .5f, 1);
        private static readonly Rect ScreenRect = new Rect(0, 0, 1, 1);

        [SerializeField] private DisplayConfiguration _displayConfiguration = DisplayConfiguration.SingleMonitorToggle;
        [SerializeField] private InputConfiguration _inputConfiguration = InputConfiguration.NoGamepad;
        [SerializeField] private Camera _vrCamera;
        [SerializeField] private Camera _monitorCamera;
        [SerializeField] private VRPlayerController _vrPlayer;
        [SerializeField] private MonitorPlayerController _monitorPlayer;
        private SelectedPlayer _selectedPlayer = SelectedPlayer.VRPlayer;
        private bool _secondDisplayActivated = false;

        protected DisplayManager() { }

        private enum SelectedPlayer
        {
            VRPlayer,
            MonitorPlayer
        }

        private void Update()
        {
            // Bind F1 to cycle DisplayConfiguration
            if (Input.GetKeyDown(KeyCode.F1))
            {
                // Enum hack to cycle through enum values
                _displayConfiguration = (DisplayConfiguration)((int)++_displayConfiguration%4);
            }
            // Bind F2 to cycle InputConfiguration
            if (Input.GetKeyDown(KeyCode.F2))
            {
                // Enum hack to cycle through enum values
                _inputConfiguration = (InputConfiguration)((int)++_inputConfiguration%2);
            }

            // Prevent incompatible configurations
            if (_inputConfiguration == InputConfiguration.Gamepad && _displayConfiguration == DisplayConfiguration.SingleMonitorToggle)
            {
                // Need to be able to see both characters if we have both control types
//                _displayConfiguration = DisplayConfiguration.SingleMonitorSplit;
            }

            // Set the correct display configuration
            switch (_displayConfiguration)
            {
                case DisplayConfiguration.VR:
                    EnableVRCamera(_displayConfiguration);
                    EnableMonitorCamera(_displayConfiguration);
                    break;
                case DisplayConfiguration.DualMonitor:
                    EnableVRCamera(_displayConfiguration);
                    EnableMonitorCamera(_displayConfiguration);
                    break;
                case DisplayConfiguration.SingleMonitorSplit:
                    EnableVRCamera(_displayConfiguration);
                    EnableMonitorCamera(_displayConfiguration);
                    break;
                case DisplayConfiguration.SingleMonitorToggle:
                    if (_selectedPlayer == SelectedPlayer.VRPlayer)
                    {
                        EnableVRCamera(_displayConfiguration);
                        DisableMonitorCamera();
                    }
                    else
                    {
                        EnableMonitorCamera(_displayConfiguration);
                        DisableVRCamera();
                    }
                    break;
            }

            // Set the correct input configuration
            switch (_inputConfiguration)
            {
                case InputConfiguration.Gamepad:
                    EnableVRControls(_inputConfiguration);
                    EnableMonitorControls(_inputConfiguration);
                    break;
                case InputConfiguration.NoGamepad:
                    if (_selectedPlayer == SelectedPlayer.VRPlayer)
                    {
                        EnableVRControls(_inputConfiguration);
                        DisableMonitorControls();
                    }
                    else
                    {
                        EnableMonitorControls(_inputConfiguration);
                        DisableVRControls();
                    }
                    break;
            }

            // Handle toggling selected character if in single monitor toggle or no gamepad
            if (_inputConfiguration == InputConfiguration.NoGamepad || _displayConfiguration == DisplayConfiguration.SingleMonitorToggle)
            {
                // Hard-code debug toggle player key to tab
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    // Enum hack to toggle selected player (rolls over to VRPlayer from MonitorPlayer)
                    _selectedPlayer = (SelectedPlayer)((int)++_selectedPlayer%2);
                }
            }
        }

        private void EnableVRCamera(DisplayConfiguration config)
        {
            _vrCamera.enabled = true;
            if (config != DisplayConfiguration.SingleMonitorSplit)
            {
                _vrCamera.rect = ScreenRect;
            }
            switch (config)
            {
                case DisplayConfiguration.VR:
                    _vrCamera.targetDisplay = 0;
                    _vrCamera.stereoTargetEye = StereoTargetEyeMask.Both;
                    break;
                case DisplayConfiguration.DualMonitor:
                    // This cannot be undone
                    if (!_secondDisplayActivated && Display.displays.Length > 1)
                    {
                        _secondDisplayActivated = true;
                        Display.displays[1].Activate(Display.displays[1].systemWidth, Display.displays[1].systemHeight, 60);
                    }
                    _vrCamera.targetDisplay = 1;
                    _vrCamera.stereoTargetEye = StereoTargetEyeMask.None;
                    break;
                case DisplayConfiguration.SingleMonitorSplit:
                    // Set up viewport on left half of the screen
                    _vrCamera.rect = VRRect;
                    _vrCamera.targetDisplay = 0;
                    _vrCamera.stereoTargetEye = StereoTargetEyeMask.None;
                    break;
                case DisplayConfiguration.SingleMonitorToggle:
                    _vrCamera.targetDisplay = 0;
                    _vrCamera.stereoTargetEye = StereoTargetEyeMask.None;
                    break;
            }
        }

        private void EnableMonitorCamera(DisplayConfiguration config)
        {
            _monitorCamera.enabled = true;
            if (config != DisplayConfiguration.SingleMonitorSplit)
            {
                _monitorCamera.rect = ScreenRect;
            }
            switch (config)
            {
                case DisplayConfiguration.VR:
                    _monitorCamera.targetDisplay = 0;
                    break;
                case DisplayConfiguration.DualMonitor:
                    _monitorCamera.targetDisplay = 0;
                    break;
                case DisplayConfiguration.SingleMonitorSplit:
                    _monitorCamera.rect = MonitorRect;
                    _monitorCamera.targetDisplay = 0;
                    break;
                case DisplayConfiguration.SingleMonitorToggle:
                    _monitorCamera.targetDisplay = 0;
                    break;
            }
        }

        private void DisableVRCamera()
        {
            _vrCamera.enabled = false;
        }

        private void DisableMonitorCamera()
        {
            _monitorCamera.enabled = false;
        }

        private void EnableVRControls(InputConfiguration config)
        {
            switch (config)
            {
                case InputConfiguration.Gamepad:
                    _vrPlayer.AllowControl = true;
                    _vrPlayer.UseKeyboardControls = false;
                    break;
                case InputConfiguration.NoGamepad:
                    _vrPlayer.AllowControl = true;
                    _vrPlayer.UseKeyboardControls = true;
                    break;
            }
        }

        private void EnableMonitorControls(InputConfiguration config)
        {
            switch (config)
            {
                case InputConfiguration.Gamepad:
                    _monitorPlayer.AllowControl = true;
                    break;
                case InputConfiguration.NoGamepad:
                    _monitorPlayer.AllowControl = true;
                    break;
            }
        }

        private void DisableVRControls()
        {
            _vrPlayer.AllowControl = false;
        }

        private void DisableMonitorControls()
        {
            _monitorPlayer.AllowControl = false;
        }
    }
}