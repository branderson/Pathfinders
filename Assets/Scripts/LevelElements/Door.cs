using Assets.Managers;
using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class Door : CustomMonoBehaviour, IAddressable
    {
        [SerializeField] private int _id;
        [SerializeField] private GameObject _doorOpen;
        [SerializeField] private GameObject _doorClose;

        private string _passcode = "";
        private bool _open = false;

        private Collider _collider;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool Open
        {
            set { _open = value; }
        }

        public string Passcode
        {
            set { _passcode = value; }
        }

        public void Start()
        {
            if (_passcode == "")
            {
                // Not locked
                EventManager.Instance.StartListening("OpenDoor " + _id + " " + _passcode, OpenDoor);
                EventManager.Instance.StartListening("CloseDoor " + _id + " " + _passcode, OpenDoor);
            }
            else
            {
                EventManager.Instance.StartListening("OpenDoor " + _id, OpenDoor);
                EventManager.Instance.StartListening("CloseDoor " + _id, OpenDoor);
            }
            if (_open)
            {
                StateOpen();
            }
            else
            {
                StateClose();
            }
        }

        public void Toggle()
        {
            if (_open)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }

        // These two functions are for unlocking the door
        private void OpenDoor()
        {
            if (_passcode != "")
            {
                // Unlock
                EventManager.Instance.StartListening("OpenDoor " + _id, OpenDoor);
                EventManager.Instance.StartListening("CloseDoor " + _id, CloseDoor);
            }
            StateOpen();
        }

        private void CloseDoor()
        {
            if (_passcode != "")
            {
                // Unlock
                EventManager.Instance.StartListening("OpenDoor " + _id, OpenDoor);
                EventManager.Instance.StartListening("CloseDoor " + _id, CloseDoor);
            }
            StateClose();
        }

        private void StateOpen()
        {
            _doorOpen.SetActive(true);
            _doorClose.SetActive(false);
            _open = true;
        }

        private void StateClose()
        {
            _doorClose.SetActive(true);
            _doorOpen.SetActive(false);
            _open = false;
        }
    }
}