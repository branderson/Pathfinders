using Assets.Utility;
using UnityEngine;

namespace Assets.VR
{

    public class FadeController : CustomMonoBehaviour
    {
        public delegate void FadeEnd();

        private bool _triggerFade = false;
        private bool _fadeIn = false;
        private float _startingTime = 0;
        private float _remainingTime = 0;
        private FadeEnd _endFunction;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (_triggerFade)
            {
                _remainingTime -= Time.deltaTime;
                Color color = _renderer.material.color;
                if (_fadeIn)
                {
                    color.a = _remainingTime/_startingTime;
                    _renderer.material.color = color;
                }
                else
                {
                    color.a = (_startingTime - _remainingTime)/_startingTime;
                    _renderer.material.color = color;
                }

                if (_remainingTime <= 0f)
                {
                    _triggerFade = false;
                    if (_endFunction != null)
                    {
                        _endFunction.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// Fade the screen in from black
        /// </summary>
        /// <param name="time">
        /// Time in seconds to fade over
        /// </param>
        /// <param name="end">
        /// Function to call when the scene has faded in completely
        /// </param>
        public void FadeIn(float time, FadeEnd end = null)
        {
            _fadeIn = true;
            _startingTime = time;
            _remainingTime = time;
            _triggerFade = true;
            _endFunction = end;
        }

        /// <summary>
        /// Fade the screen to black
        /// </summary>
        /// <param name="time">
        /// Time in seconds to fade over
        /// </param>
        /// <param name="end">
        /// Function to call when the scene has faded out completely
        /// </param>
        public void FadeOut(float time, FadeEnd end = null)
        {
            _fadeIn = false;
            _startingTime = time;
            _remainingTime = time;
            _triggerFade = true;
            _endFunction = end;
        }
    }
}