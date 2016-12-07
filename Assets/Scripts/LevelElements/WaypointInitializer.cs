using TiledLoader;
using UnityEngine;

namespace Assets.LevelElements
{
    [ExecuteInEditMode]
    public class WaypointInitializer : MonoBehaviour
    {
        private void HandleInstanceProperties()
        {
            Waypoint waypoint = GetComponent<Waypoint>();
            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
            properties.TryGetInt("ID", out waypoint.ID);
            properties.TryGetInt("NextID", out waypoint.NextID);
            properties.TryGetInt("PreviousID", out waypoint.PreviousID);
        }
    }
}