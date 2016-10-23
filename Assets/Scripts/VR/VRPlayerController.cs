using UnityEngine;
using Assets.Utility;
using Assets.Utility.Static;

namespace Assets.VR
{
	public class VRPlayerController : CustomMonoBehaviour
	{
	    [SerializeField] private float _moveSpeed = 5;
	    [SerializeField] private bool _useGamepadLook = true;

	    private Transform _cameraTransform;
	    private Camera _camera;
	    private CharacterController _controller;
	    private SmoothMouseLook _mouseLook;

        // Development
	    private bool _useKeyboardControls = false;
	    private bool _allowControl = true;

        /// <summary>
        /// Whether the VR player is controlled by the keyboard instead of the gamepad
        /// </summary>
	    public bool UseKeyboardControls
	    {
	        set { _useKeyboardControls = value; }
	    }

        /// <summary>
        /// Whether the VR player can use the gamepad's right stick to look around
        /// </summary>
	    public bool UseGamepadLook
	    {
	        set { _useGamepadLook = value; }
	    }

        /// <summary>
        /// Whether the VR player can be controlled
        /// </summary>
	    public bool AllowControl
	    {
            set
            {
                _allowControl = value;
                if (_allowControl)
                {
                    _mouseLook.enabled = true;
                }
                else
                {
                    _mouseLook.enabled = false;
                }
            }
	    }

	    private void Awake()
	    {
	        _camera = GetComponentInChildren<Camera>();
	        _cameraTransform = _camera.transform;
	        _controller = GetComponent<CharacterController>();
	        _mouseLook = GetComponentInChildren<SmoothMouseLook>();
	    }

	    private void Update()
	    {
	        if (!_allowControl) return;

            // Get input
	        float hor = 0;
	        float ver = 0;
	        if (_useKeyboardControls)
	        {
	            hor = Input.GetAxis("Horizontal");
	            ver = Input.GetAxis("Vertical");
	            _mouseLook.UseGamepadControls = false;
	        }
	        else
	        {
	            hor = Input.GetAxis("VRHorizontal");
	            ver = Input.GetAxis("VRVertical");
                _mouseLook.UseGamepadControls = _useGamepadLook;
	        }

            // Rotate movement vector by VR camera's y rotation
            Vector3 move = new Vector3(hor, 0, ver);
	        move = Quaternion.Euler(0, _cameraTransform.localRotation.eulerAngles.y, 0) * move;
	        move.y = 0;

//            transform.AdjustLocalPosition(_moveSpeed*move.x*Time.deltaTime, 0, _moveSpeed*move.z*Time.deltaTime);
	        _controller.SimpleMove(move*_moveSpeed);
	    }
	}
} 