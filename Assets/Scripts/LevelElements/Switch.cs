using System.Collections.Generic;
using Assets.Managers;
using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class Switch : CustomMonoBehaviour
    {
        [SerializeField] private int _id;
        public List<int> DoorIDs = new List<int>();

        private bool _on = false;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public void Toggle()
        {
            foreach (int id in DoorIDs)
            {
                DoorManager.Instance.GetDoor(id).Toggle();
            }
            _on = !_on;
        }
    }
}