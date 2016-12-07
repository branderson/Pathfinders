using Assets.Enemies;
using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class Waypoint : CustomMonoBehaviour {
        public int ID;
        public int NextID;
        public int PreviousID;

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

        private void OnTriggerEnter(Collider col)
        {
            print(col.gameObject.name);
            if (col.gameObject.name != "Model") return;
            EnemyAI enemy = col.GetComponentInParent<EnemyAI>();
            if (enemy == null) return;
            enemy.WaypointTouched(this);
        }
    }
}
