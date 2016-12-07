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
            int id;
            properties.TryGetString("Code", out text);
            properties.TryGetInt("ID", out id);
            comp.Text = "Door: " + id +  "\n" + "Code: " + text;
            DestroyImmediate(this);
        }
    }
}