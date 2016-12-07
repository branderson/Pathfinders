using TiledLoader;
using UnityEngine;

namespace Assets.LevelElements
{
    [ExecuteInEditMode]
    public class WaypointInitializer : MonoBehaviour
    {
        private void HandleInstanceProperties()
        {
            // Can initialize in class because no callbacks implemented
            Waypoint waypoint = GetComponent<Waypoint>();
            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
            properties.TryGetInt("ID", out waypoint.ID);
            properties.TryGetInt("NextID", out waypoint.NextID);
            properties.TryGetInt("PreviousID", out waypoint.PreviousID);
        }
    }
}