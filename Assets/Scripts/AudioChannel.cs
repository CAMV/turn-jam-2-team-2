using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnJam2
{
    public class AudioChannel : MonoBehaviour
    {
        public AudioSource audioSource;
        public SoundCategory category;

        public void PlayOnce(AudioClip clip, float volume = 1f)
        {
            audioSource.PlayOneShot(clip, volume);
        }

        public void Play(Sound sound)
        {
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume;
            audioSource.pitch = sound.pitch;
            audioSource.Play();
        }

        public void Stop()
        {
            if (!audioSource.isPlaying) return;
            audioSource.Stop();
        }
    }
}