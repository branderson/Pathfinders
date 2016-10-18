using UnityEngine;
using Assets.Utility;
using Assets.Utility.Static;

namespace Assets.VR
{
	public class VRPlayerController : CustomMonoBehaviour
	{
	    [SerializeField] private bool _useKeyboardControls = false;
	    [SerializeField] private float _moveSpeed = 5;

	    private Transform _cameraTransform;
	    private Camera _camera;

	    private void Awake()
	    {
	        _camera = GetComponentInChildren<Camera>();
	        _cameraTransform = _camera.transform;
	    }

	    private void Update()
	    {
	        float hor = Input.GetAxis("VRHorizontal");
	        float ver = Input.GetAxis("VRVertical");
	        if (_useKeyboardControls)
	        {
	            hor += Input.GetAxis("Horizontal");
	            ver += Input.GetAxis("Vertical");
	        }

            // Rotate movement vector by VR camera's y rotation
            Vector3 move = new Vector3(hor, 0, ver);
	        move = Quaternion.Euler(0, _cameraTransform.localRotation.eulerAngles.y, 0) * move;

            transform.AdjustLocalPosition(_moveSpeed*move.x*Time.deltaTime, 0, _moveSpeed*move.z*Time.deltaTime);
	    }
	}
} 