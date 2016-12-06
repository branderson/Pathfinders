using TiledLoader;
using Assets.Utility;
using UnityEngine;

namespace Assets.Enemies
{
    [ExecuteInEditMode]
    public class EnemyController : CustomMonoBehaviour
    {
        private void HandleInstanceProperties()
        {
            //            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
        }
        void OnControllerColliderHit(ControllerColliderHit col)
        {
            if (col.gameObject.name == "Cube")
                print("Game Over");
        }
    }
}