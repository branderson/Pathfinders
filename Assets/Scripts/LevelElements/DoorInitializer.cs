using TiledLoader;
using UnityEngine;

namespace Assets.LevelElements
{
    [ExecuteInEditMode]
    public class DoorInitializer : MonoBehaviour
    {
        public void HandleInstanceProperties()
        {
            Door door = GetComponent<Door>();
            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
            int id;
            bool open;
            string passcode;
            properties.TryGetInt("ID", out id);
            properties.TryGetBool("Open", out open);
            properties.TryGetString("Passcode", out passcode);
            door.ID = id;
            door.Open = open;
            door.Passcode = passcode;
            DestroyImmediate(this);
        }
    }
}