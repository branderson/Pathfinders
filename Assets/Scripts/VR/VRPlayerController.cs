using UnityEngine;
using Assets.Utility;
using Assets.Utility.Static;
using UnityEngine.SceneManagement;

namespace Assets.VR
{
	public class VRPlayerController : CustomMonoBehaviour
	{
	    [SerializeField] private float _moveSpeed = 5;          // Player speed in m/sec
	    [SerializeField] private bool _useGamepadLook = true;
	    [SerializeField] private bool _smoothMouseLook = false;

	    private Transform _cameraTransform;
	    private Camera _camera;
	    private CharacterController _controller;
	    private SmoothMouseLook _mouseLook;
	    private FixedIncrementLook _fixedLook;
	    private FadeController _fade;
	    private FadeController _monitorFade;

	    private bool _lockFade = false;

        // Development
	    private bool _useKeyboardControls = false;
	    private bool _allowControl = true;

	    public bool LockMovement = false;

	    public Transform CameraTransform
	    {
	        get { return _cameraTransform; }
	    }

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
        /// Whether the VR Players view should be smooth or "jump" in 90 degree increments when using the
        /// gamepad for look
        /// </summary>
	    public bool SmoothMouseLook
	    {
            set
            {
                _smoothMouseLook = value;
            }
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
	        _fixedLook = GetComponent<FixedIncrementLook>();
	        _fade = GetComponentInChildren<FadeController>();
	    }

	    private void Start()
	    {
	        _fade.FadeIn(2.5f);
	        _monitorFade = GameObject.FindGameObjectWithTag("MonitorPlayer").GetComponentInChildren<FadeController>();
	        _monitorFade.FadeIn(2.5f);
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
	            _fixedLook.enabled = false;
	        }
	        else
	        {
	            hor = Input.GetAxis("VRHorizontal");
	            ver = Input.GetAxis("VRVertical");
	            if (_smoothMouseLook)
	            {
	                _mouseLook.enabled = true;
	                _fixedLook.enabled = false;
	                _mouseLook.UseGamepadControls = _useGamepadLook;
	            }
	            else
	            {
	                _mouseLook.enabled = false;
	                _fixedLook.enabled = true;
	            }
	        }

	        if (Input.GetButtonDown("VRInteract"))
	        {
	            Collider[] cols = Physics.OverlapSphere(transform.position, .5f);
	            foreach (Collider col in cols)
	            {
	                col.SendMessageUpwards("Interact", SendMessageOptions.DontRequireReceiver);
	            }
	        }

            // Rotate movement vector by VR camera's y rotation
            Vector3 move = new Vector3(hor, 0, ver);
	        move = Quaternion.Euler(0, _cameraTransform.localRotation.eulerAngles.y, 0) * transform.rotation * move;
	        move.y = 0;

//            transform.AdjustLocalPosition(_moveSpeed*move.x*Time.deltaTime, 0, _moveSpeed*move.z*Time.deltaTime);
	        if (!LockMovement)
	        {
                _controller.SimpleMove(move*_moveSpeed);
	        }
	    }

	    public void Win()
	    {
	        if (_lockFade) return;
	        _lockFade = true;
	        AllowControl = false;
            _fade.FadeOut(5, ReturnToMenu);
            _monitorFade.FadeOut(5);
	    }

	    public void Die()
	    {
	        if (_lockFade) return;
	        _lockFade = true;
	        AllowControl = false;
            _fade.FadeOut(2.5f, ReturnToMenu);
            _monitorFade.FadeOut(2.5f);
	    }

	    public void StartGame()
	    {
	        if (_lockFade) return;
	        _lockFade = true;
            _fade.FadeOut(2.5f, GoToGame);
            _monitorFade.FadeOut(2.5f);
	    }

	    public void QuitGame()
	    {
	        if (_lockFade) return;
	        _lockFade = true;
            _fade.FadeOut(2.5f, ReturnToMenu);
            _monitorFade.FadeOut(2.5f);
	    }

	    private void GoToGame()
	    {
	        SceneManager.LoadScene("GameScene");
	    }

	    private void ReturnToMenu()
	    {
	        SceneManager.LoadScene("MenuScene");
	    }
	}
} 