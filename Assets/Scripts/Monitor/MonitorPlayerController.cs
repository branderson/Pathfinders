using UnityEngine;
using System.Collections;
using Assets.Utility;
using Assets.Utility.Static;

namespace Assets.Monitor
{
	public class MonitorPlayerController : CustomMonoBehaviour
	{
	    [SerializeField] private float _moveSpeed = 5f;
	    [SerializeField] private float _vertSpeed = 5f;
	    private bool _allowControl = true;

	    private Rigidbody _rigidbody;
	    private CharacterController _controller;

        /// <summary>
        /// Whether the monitor player can be controlled
        /// </summary>
	    public bool AllowControl
	    {
	        set { _allowControl = value; }
	    }

	    private void Awake()
	    {
//	        _rigidbody = GetComponent<Rigidbody>();
	        _controller = GetComponent<CharacterController>();
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