using System;
using UnityEngine;
using UnityEngine.Events;

namespace TurnJam2
{
    public class FallRespawner : MonoBehaviour
    {
        public event Action OnFall;

        [SerializeField] 
        private string _fallSoundName;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out _))
            {
                Locator.AudioHandler.Play(_fallSoundName, true);
                OnFall?.Invoke();
            }
        }
    }
}