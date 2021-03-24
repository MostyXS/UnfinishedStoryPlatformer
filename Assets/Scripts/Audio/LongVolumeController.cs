using Game.PreferencesControl;
using UnityEngine;

namespace Game.Audio
{
    public class LongVolumeController : MonoBehaviour
    {
        //TO DO
        //public static AudioSource MusicSource { get; private set; }
        
        [Header("All relevant audio sources, except one shot audios")] [SerializeField]
        PrefKey volumeKey;

        private AudioSource[] _audioSources;

        private void Awake()
        {
            _audioSources = GetComponents<AudioSource>();
        }

        private void Start()
        {
            UpdateVolume();
            //if (volumeKey == PrefKey.MusicVolume)
            //    MusicSource = GetComponent<AudioSource>();
        }

        public void Stop()
        {
            foreach (var audioSource in _audioSources)
            {
                audioSource.Stop();
            }
        }

        public void Play()
        {
            foreach (var audioSource in _audioSources)
            {
                audioSource.Play();
            }
        }

        private void UpdateVolume()
        {
            foreach (var audioSource in _audioSources)
            {
                audioSource.volume = PrefsController.GetVolumeByKey(volumeKey);
            }
        }

        public void OnVolumeChanged(float volume) // defaults event;
        {
            UpdateVolume();
        }

        public PrefKey GetVolumeKey() => volumeKey;
    }
}