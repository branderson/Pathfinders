using UnityEngine;
using System.Collections;
using Assets.Utility;

namespace Assets.Monitor
{
	public class MonitorPlayerController : CustomMonoBehaviour
	{
	    private bool _allowControl = true;

        /// <summary>
        /// Whether the monitor player can be controlled
        /// </summary>
	    public bool AllowControl
	    {
	        set { _allowControl = value; }
	    }
	}
} 