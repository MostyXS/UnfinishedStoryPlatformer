using System.Collections;
using UnityEngine;

namespace Game.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private Coroutine _currentFade;

        private static CanvasGroup _canvasGroup;
        private static Fader _fader;

        public static Fader Instance
        {
            get
            {
                if (_fader == null)
                    Debug.LogError("There is no fader on scene");
                return _fader;
            }
            private set => _fader = value;
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Instance = this;
        }

        public void FadeOutImmediate()
        {
            _canvasGroup.alpha = 1;
        }

        public void FadeInImmediate()
        {
            _canvasGroup.alpha = 0;
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(time, 0);
        }

        public Coroutine FadeOut(float time)
        {
            return Fade(time, 1);
        }

        private Coroutine Fade(float time, float target)
        {
            if (_currentFade != null)
            {
                StopCoroutine(_currentFade);
            }

            _currentFade = StartCoroutine(FadeRoutine(time, target));
            return _currentFade;
        }

        IEnumerator FadeRoutine(float time, float target)
        {
            while (!Mathf.Approximately(_canvasGroup.alpha, target))
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}