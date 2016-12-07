using TiledLoader;
using UnityEngine;

namespace Assets.LevelElements
{
    [ExecuteInEditMode]
    public class PasscodeInitializer : MonoBehaviour
    {
        private void HandleInstanceProperties()
        {
            LoadedText comp = GetComponent<LoadedText>();
            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
            string text;
            properties.TryGetString("Passcode", out text);
            comp.Text = text;
            DestroyImmediate(this);
        }
    }
}