using TiledLoader.Editor;
using UnityEditor;

namespace Assets.Extensions.TiledLoader.Editor
{
    public class TiledLoader
    {
        [MenuItem("Window/TiledLoader")]
        public static void OpenWindow()
        {
            TiledLoaderWindow.OpenWindow();
        }
    }
}