using System;
using UnityEngine;
using UnityEngine.Events;

namespace TurnJam2
{
    public class FallRespawner : MonoBehaviour
    {
        public event Action OnFall;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out _))
            {
                Debug.Log("Invoke!!!");
                OnFall?.Invoke();
            }
        }
    }
}