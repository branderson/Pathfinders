using System.Collections.Generic;
using Assets.Managers;
using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class Switch : CustomMonoBehaviour, IAddressable
    {
        [SerializeField] private int _id;
        public List<int> DoorIDs = new List<int>();

        private MeshRenderer _renderer;
        private bool _on = false;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private void Awake()
        {
            _renderer = GetComponentInChildren<MeshRenderer>();
        }

        private void Start()
        {
            foreach (int id in DoorIDs)
            {
                DoorManager.Instance.GetDoor(id).SwitchControlled = true;
            }
        }

        public void Toggle()
        {
            if (_on) return;
            foreach (int id in DoorIDs)
            {
                DoorManager.Instance.GetDoor(id).Toggle();
            }
            // Rotate to flip switch position
            _renderer.transform.Rotate(new Vector3(0, 0, 180));
            _on = !_on;
        }

        public void Interact()
        {
            Toggle();
        }
    }
}