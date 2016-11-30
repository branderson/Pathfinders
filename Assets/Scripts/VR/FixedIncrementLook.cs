using Assets.Utility;
using UnityEngine;

namespace Assets.VR
{
    public class FixedIncrementLook : CustomMonoBehaviour
    {
//        private Camera _camera;
//        private Transform _cameraTransform;

        private float _lastDirection = 0f;

        private void Awake()
        {
//            _camera = GetComponentInChildren<Camera>();
//            _cameraTransform = _camera.transform;
        }

        private void Update()
        {
            float hor = Input.GetAxisRaw("VRLookHorizontal");

            if (hor < .1f && hor > -.1f)
            {
                _lastDirection = 0f;
            }
            else if (_lastDirection <= 0f && hor > .75f)
            {
                _lastDirection = 1f;
                RotateRight();
            }
            else if (_lastDirection >= 0f && hor < -.75f)
            {
                _lastDirection = -1f;
                RotateLeft();
            }
        }

        private void RotateRight()
        {
            transform.Rotate(transform.up, 90, Space.World);
        }

        private void RotateLeft()
        {
            transform.Rotate(transform.up, -90, Space.World);
        }
    }
}