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

        [SerializeField] private string _passcode = "";
        [SerializeField] private bool _open = false;
        [SerializeField] private bool _switchControlled = false;

        private LoadedText _text;
        private FloatingID _floatingID;

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

        public bool SwitchControlled
        {
            set
            {
                _switchControlled = value;
                if (value)
                {
                    _floatingID.SetText("[Switch] Door " + _id);
                }
            }
        }

        private void Awake()
        {
            _text = GetComponent<LoadedText>();
            _floatingID = GetComponentInChildren<FloatingID>();
        }

        public void Start()
        {
            if (!_switchControlled)
            {
                EventManager.Instance.StartListening("OpenDoor" + _id + _passcode, OpenDoor);
                EventManager.Instance.StartListening("CloseDoor" + _id + _passcode, CloseDoor);
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
            if (_passcode != "" && !_switchControlled)
            {
                // Unlock
                EventManager.Instance.StartListening("OpenDoor" + _id, OpenDoor);
                EventManager.Instance.StartListening("CloseDoor" + _id, CloseDoor);
                _floatingID.SetText("Door " + _id);
            }
            StateOpen();
        }

        private void CloseDoor()
        {
            if (_passcode != "" && !_switchControlled)
            {
                // Unlock
                EventManager.Instance.StartListening("OpenDoor" + _id, OpenDoor);
                EventManager.Instance.StartListening("CloseDoor" + _id, CloseDoor);
                _floatingID.SetText("Door " + _id);
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