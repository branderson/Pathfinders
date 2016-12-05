using UnityEngine;
using System.Collections;
using Assets.Utility;
using Assets.Utility.Static;
using Assets.VR;

namespace Assets.Monitor
{
	public class MonitorPlayerController : CustomMonoBehaviour
	{
	    [SerializeField] private float _moveSpeed = 5f;
	    [SerializeField] private float _vertSpeed = 5f;
	    [SerializeField] private float _minimumHeight = 5f;
	    [SerializeField] private float _maximumHeight = 25f;
	    [SerializeField] private bool _canEnable = true;
	    private bool _allowControl = true;

	    private Rigidbody _rigidbody;
	    private CharacterController _controller;
	    private SmoothMouseLook _mouseLook;
//	    private MonitorMenuController _menu;

	    public bool LockedControl = false;

        /// <summary>
        /// Whether the monitor player can be controlled
        /// </summary>
	    public bool AllowControl
	    {
            set
            {
                if (!_canEnable) return;
                if (LockedControl) return;
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
//	        _rigidbody = GetComponent<Rigidbody>();
	        _controller = GetComponent<CharacterController>();
	        _mouseLook = GetComponent<SmoothMouseLook>();
//	        _menu = GetComponent<MonitorMenuController>();
	        if (!_canEnable)
	        {
	            _allowControl = false;
	        }
	    }

	    private void Update()
	    {
	        if (!_allowControl) return;

	        float hor = Input.GetAxis("Horizontal");
	        float ver = Input.GetAxis("Vertical");
	        float alt = Input.GetAxis("Altitude");

            Vector3 move = new Vector3(hor*_moveSpeed, 0, ver*_moveSpeed) * Time.deltaTime;
	        move = transform.rotation * move;
	        if (!Mathf.Approximately(alt, 0))
	        {
	            move.y = alt*_vertSpeed*Time.deltaTime;
	        }

            // Constrain position along Y-axis
	        if (transform.position.y + move.y > _maximumHeight) move.y = _maximumHeight - transform.position.y;
	        else if (transform.position.y + move.y < _minimumHeight) move.y = _minimumHeight - transform.position.y;

	        _controller.Move(move);
	    }
	}
} 