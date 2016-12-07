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
            properties.TryGetString("Code", out text);
            comp.Text = "Door: " + "\n" + "Code: " + text;
            DestroyImmediate(this);
        }
    }
}