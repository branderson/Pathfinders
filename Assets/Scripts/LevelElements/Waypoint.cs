using TiledLoader;
using UnityEngine;

namespace Assets.LevelElements
{
    [ExecuteInEditMode]
    public class Waypoint : MonoBehaviour {
        public int ID;
        public int NextID;
        public int PreviousID;

        private void HandleInstanceProperties()
        {
            // Can initialize in class because no callbacks implemented
            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
            properties.TryGetInt("ID", out ID);
            properties.TryGetInt("NextID", out NextID);
            properties.TryGetInt("PreviousID", out PreviousID);
        }

        public int Next() {
            if (NextID == 0)
                return -PreviousID;
            return NextID;
        }

        public int Previous()
        {
            if (PreviousID == 0)
                return -NextID;
            return PreviousID;
        }
    }
}
