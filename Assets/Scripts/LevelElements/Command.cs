using Assets.Monitor;
using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class Command : CustomMonoBehaviour
    {
        [SerializeField] private GameObject _mask;
        [SerializeField] private GameObject _commandText;
        private bool _activated = false;

        public string Text;

        private void Start()
        {
            _mask.SetActive(true);
            _commandText.SetActive(false);
        }

        public void Interact()
        {
            if (!_activated)
            {
                _activated = true;
                _commandText.SetActive(true);
                _mask.SetActive(false);
                GameObject.FindGameObjectWithTag("MonitorPlayer").GetComponent<MonitorPlayerController>().AddScriptInfo(Text);
            }
        }
    }
}