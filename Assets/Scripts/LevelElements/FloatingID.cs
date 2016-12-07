using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class FloatingID : CustomMonoBehaviour
    {
        [SerializeField] private float _height = 1f;

        private IAddressable _addressable;

        private void Awake()
        {
            _addressable = GetComponent<IAddressable>();
        }

        private void Update()
        {
            // TODO: Draw the floating text here
        }
    }
}