using Assets.Monitor;
using Assets.Monitor.Terminal;
using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class Passcode : CustomMonoBehaviour
    {
        [SerializeField] private GameObject _mask;
        [SerializeField] private GameObject _passcodeText;

        public int ID;
        public string Code;

        private bool _activated = false;

        private void Start()
        {
            _mask.SetActive(true);
            _passcodeText.SetActive(false);
        }

        public void Interact()
        {
            if (!_activated)
            {
                _activated = true;
                _passcodeText.SetActive(true);
                _mask.SetActive(false);
                GameObject.FindGameObjectWithTag("MonitorPlayer").GetComponent<MonitorPlayerController>().AddPasscode(ID, Code);
            }
        }
    }
}