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
            properties.TryGetInt("ID", out ai.ID);
            properties.TryGetInt("Destination", out ai.Destination);
            DestroyImmediate(this);
        }

    }
}