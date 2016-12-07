using TiledLoader;
using UnityEngine;

namespace Assets.LevelElements
{
    [ExecuteInEditMode]
    public class SwitchInitializer : MonoBehaviour
    {
        public void HandleInstanceProperties()
        {
            Switch sw = GetComponent<Switch>();
            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
            int id;
            string doors;
            properties.TryGetInt("ID", out id);
            properties.TryGetString("DoorIDs", out doors);
            sw.ID = id;
            DestroyImmediate(this);
        }
    }
}