using Assets.Utility;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager() { }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
        }
    }
}