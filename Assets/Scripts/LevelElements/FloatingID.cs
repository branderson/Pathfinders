using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class FloatingID : CustomMonoBehaviour
    {
        [SerializeField] private string name = "";
        [SerializeField] private bool useID = true;
        []

        private IAddressable _addressable;

        private void Awake()
        {
            _addressable = GetComponentInParent<IAddressable>();
            if (useID)
                gameObject.GetComponent<TextMesh>().text = name + _addressable.ID;
            else
                gameObject.GetComponent<TextMesh>().text = ;
        }

        private void Update()
        {
            
        }
    }
}