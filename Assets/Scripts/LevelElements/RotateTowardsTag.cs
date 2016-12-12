using Assets.Utility;
using UnityEngine;

namespace Assets.LevelElements
{
    public class RotateTowardsTag : CustomMonoBehaviour
    {
        [SerializeField] private string _tag;

        private Transform _target;

        private void Awake()
        {
            _target = GameObject.FindGameObjectWithTag(_tag).transform;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(_target.forward, _target.up);
        }
    }
}