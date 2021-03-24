using UnityEngine;

namespace Game.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip[] music;
        private AudioSource _audioSource;

        //private int _currentPlaying = 0;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /*private void Update()
        {
            PlayMusic();
        }*/

        // private void PlayMusic()
        // {
        //     if (_audioSource.isPlaying) return;
        //
        //     if (_currentPlaying != music.Length - 1)
        //     {
        //         _audioSource.clip = music[_currentPlaying + 1];
        //         _audioSource.Play();
        //         _currentPlaying++;
        //     }
        //     else
        //     {
        //         _audioSource.clip = music[0];
        //         _audioSource.Play();
        //         _currentPlaying = 0;
        //     }
        // }
    }
}