using System;
using UnityEngine;

namespace TurnJam2
{
    public class CheckPointSaver : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out _))
            {
                Locator.GameManager.AssignCheckPoint(this);
            }
        }

        public void ReSpawn(Player _player)
        {
            Debug.Log("Respawn");
            _player.WarpToPosition(transform.position);
        }
    }
}