using TiledLoader;
using UnityEngine;

namespace Assets.Enemies
{
    [ExecuteInEditMode]
    public class EnemyInitializer : MonoBehaviour
    {
        private void HandleInstanceProperties()
        {
            EnemyAI ai = GetComponent<EnemyAI>();
            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
            int id;
            properties.TryGetInt("ID", out id);
            ai.ID = id;
            properties.TryGetInt("Destination", out ai.Destination);
            DestroyImmediate(this);
        }

    }
}