using Game.Utils;
using UnityEngine;

namespace Game.Audio
{
    public class MainAudioSource : MonoBehaviour
    {
        public static LazyValue<AudioSource> Instance { get; private set; }


        private void Awake()
        {
            Instance = new LazyValue<AudioSource>(GetInitialAudio);
            Instance.ForceInit();
        }

        public AudioSource GetInitialAudio()
        {
            return GetComponent<AudioSource>();
        }
    }
}