using System;
using UnityEngine;

namespace TurnJam2
{
  [Serializable]
  public class Sound
  {
    public string name;
    public AudioClip clip;
    public SoundCategory category;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
  }

  public enum SoundCategory
  {
    Music,
    SFX,  // Sound Effects
    UI
  }
}