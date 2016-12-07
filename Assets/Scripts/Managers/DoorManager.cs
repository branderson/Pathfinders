using System.Collections.Generic;
using Assets.LevelElements;
using Assets.Utility;
using UnityEngine.SceneManagement;


namespace Assets.Managers
{
    public class DoorManager : Singleton<DoorManager>
    {
        private Dictionary<int, Door> _doors;

        protected DoorManager() { }

        private void Start()
        {
            SceneManager.sceneLoaded += OnLevelLoad;
        }

        private void OnLevelLoad(Scene scene, LoadSceneMode mode) 
        {
            LoadDoors();
        }

        private void LoadDoors()
        {
            _doors = new Dictionary<int, Door>();
            Door[] doors = FindObjectsOfType<Door>();

            foreach (Door door in doors)
            { 
                _doors[door.ID] = door;
            }
        }

        public Door GetDoor(int id)
        {
            if (_doors == null) LoadDoors();
            return id == 0 ? null : _doors[id];
        }
    }
}
