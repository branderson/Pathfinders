using Assets.Monitor.Terminal;
using TiledLoader;
using UnityEngine;

namespace Assets.LevelElements
{
    [ExecuteInEditMode]
    public class CommandInitializer : MonoBehaviour
    {
        private void HandleInstanceProperties()
        {
            LoadedText comp = GetComponent<LoadedText>();
            Command command = GetComponent<Command>();
            TiledLoaderProperties properties = GetComponent<TiledLoaderProperties>();
            int id;
            properties.TryGetInt("ID", out id);
            comp.Text = TerminalCommands.Commands[id];
            command.Text = comp.Text;
            DestroyImmediate(this);
        }
    }
}