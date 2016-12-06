using Assets.Utility;
using Assets.Utility.Static;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.VR
{
    public class VRMenuController : CustomMonoBehaviour
    {
        [SerializeField] private VRPlayerController _playerController;
        [SerializeField] private Image _instructionPanel;
        [SerializeField] private float _distance = 2.4f;

        private void Awake()
        {
            _instructionPanel.transform.localPosition = new Vector3(0, 0, _distance);
            _instructionPanel.gameObject.SetActive(false);
        }

        public void Update()
        {
            // Find nearest 90 degree increment
            float playerRotation = _playerController.CameraTransform.rotation.eulerAngles.y;
            playerRotation = Mathf.Round(playerRotation / 90) * 90f;
            transform.rotation = Quaternion.AngleAxis(playerRotation, Vector3.up);
        }

        public void OpenPauseMenu()
        {
            _playerController.LockMovement = true;
        }

        public void OpenInstructionMenu()
        {
            _instructionPanel.gameObject.SetActive(true);
        }

        public void CloseInstructionMenu()
        {
            _instructionPanel.gameObject.SetActive(false);
        }

        public void CloseMenu()
        {
            _playerController.LockMovement = false;
        }
    }
}