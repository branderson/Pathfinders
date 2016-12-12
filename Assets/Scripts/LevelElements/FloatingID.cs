using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class FloatingID : CustomMonoBehaviour
    {
        [SerializeField] private string _name = "";
        [SerializeField] private bool useID = true;

        private IAddressable _addressable;
        private TextMesh _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMesh>();
            _addressable = GetComponentInParent<IAddressable>();
            if (useID)
            {
                _textMesh.text = _name + " " + _addressable.ID;
            }
            else
            {
                _textMesh.text = GetComponentInParent<LoadedText>().Text;
            }
        }

        public void SetText(string text)
        {
            _textMesh.text = text;
        }

        private void Update()
        {
            
        }
    }
}