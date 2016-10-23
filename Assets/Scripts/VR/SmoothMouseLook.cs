using System.Collections.Generic;
using UnityEngine;

namespace Assets.VR
{
    public class SmoothMouseLook : MonoBehaviour {
 
        public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;
        private bool _useGamepadControls = false;
 
        public float minimumX = -360F;
        public float maximumX = 360F;
 
        public float minimumY = -60F;
        public float maximumY = 60F;
 
        private float rotationX = 0F;
        private float rotationY = 0F;
 
        private List<float> rotArrayX = new List<float>();
        private float rotAverageX = 0F;	
 
        private List<float> rotArrayY = new List<float>();
        private float rotAverageY = 0F;
 
        private float frameCounter = 20;
 
        private Quaternion originalRotation;

        /// <summary>
        /// Whether the gamepad's right joystick should control the camera, false is mouse control
        /// </summary>
        public bool UseGamepadControls
        {
            set { _useGamepadControls = value; }
        }
 
        void Update ()
        {
            if (axes == RotationAxes.MouseXAndY)
            {			
                rotAverageY = 0f;
                rotAverageX = 0f;
 
                if (_useGamepadControls)
                {
                    rotationY += Input.GetAxis("VRLookVertical")*sensitivityY;
                    rotationX += Input.GetAxis("VRLookHorizontal")*sensitivityX;
                }
                else
                {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                }
 
                rotArrayY.Add(rotationY);
                rotArrayX.Add(rotationX);
 
                if (rotArrayY.Count >= frameCounter) {
                    rotArrayY.RemoveAt(0);
                }
                if (rotArrayX.Count >= frameCounter) {
                    rotArrayX.RemoveAt(0);
                }
 
                for(int j = 0; j < rotArrayY.Count; j++) {
                    rotAverageY += rotArrayY[j];
                }
                for(int i = 0; i < rotArrayX.Count; i++) {
                    rotAverageX += rotArrayX[i];
                }
 
                rotAverageY /= rotArrayY.Count;
                rotAverageX /= rotArrayX.Count;
 
                rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
                rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
 
                Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
                Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
 
                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
            }
            else if (axes == RotationAxes.MouseX)
            {			
                rotAverageX = 0f;
 
                if (_useGamepadControls)
                {
                    rotationX += Input.GetAxis("VRLookHorizontal")*sensitivityX;
                }
                else
                {
                    rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                }
 
                rotArrayX.Add(rotationX);
 
                if (rotArrayX.Count >= frameCounter) {
                    rotArrayX.RemoveAt(0);
                }
                for(int i = 0; i < rotArrayX.Count; i++) {
                    rotAverageX += rotArrayX[i];
                }
                rotAverageX /= rotArrayX.Count;
 
                rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
 
                Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;			
            }
            else
            {			
                rotAverageY = 0f;
 
                if (_useGamepadControls)
                {
                    rotationY += Input.GetAxis("VRLookVertical")*sensitivityY;
                }
                else
                {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                }
 
                rotArrayY.Add(rotationY);
 
                if (rotArrayY.Count >= frameCounter) {
                    rotArrayY.RemoveAt(0);
                }
                for(int j = 0; j < rotArrayY.Count; j++) {
                    rotAverageY += rotArrayY[j];
                }
                rotAverageY /= rotArrayY.Count;
 
                rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
 
                Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }
 
        void Start ()
        {		
            Rigidbody rb = GetComponent<Rigidbody>();	
            if (rb)
                rb.freezeRotation = true;
            originalRotation = transform.localRotation;
        }
 
        public static float ClampAngle (float angle, float min, float max)
        {
            angle = angle % 360;
            if ((angle >= -360F) && (angle <= 360F)) {
                if (angle < -360F) {
                    angle += 360F;
                }
                if (angle > 360F) {
                    angle -= 360F;
                }			
            }
            return Mathf.Clamp (angle, min, max);
        }
    }
}