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
	    private bool _allowControl = true;

	    private Rigidbody _rigidbody;
	    private CharacterController _controller;
	    private SmoothMouseLook _mouseLook;

        /// <summary>
        /// Whether the monitor player can be controlled
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
//	        _rigidbody = GetComponent<Rigidbody>();
	        _controller = GetComponent<CharacterController>();
	        _mouseLook = GetComponent<SmoothMouseLook>();
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
	        _controller.Move(move);
	    }
	}
} 