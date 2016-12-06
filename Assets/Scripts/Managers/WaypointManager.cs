using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Utility;


namespace Assets.Managers
{
    public class WaypointManager : Singleton<WaypointManager>
    {
        private Dictionary<int, Waypoint> _waypoints;

        protected WaypointManager() { }

        public Waypoint GetWaypoint(int id) { if (id == 0) return null; return _waypoints[id]; }

        private void Awake() 
        {
            _waypoints = new Dictionary<int, Waypoint>();
            Object[] wps = Object.FindObjectsOfType(typeof(Waypoint));
            for (int x = 0; x < wps.Length; x++) {
                Waypoint wp = (Waypoint) wps[x];
                _waypoints.Add(wp.ID, wp);
            }
        }
    }
}
