using UnityEngine;

namespace TurnJam2
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private FallRespawner _fallRespawner;
        [SerializeField] private CheckPointSaver _fallbackCheckpoint;
        
        private CheckPointSaver _checkPoint;

        public Player Player => _player;
    
        private void Awake()
        {
            Locator.ProvideGameManager(this);
            _fallRespawner.OnFall += RespawnPlayer;
        }

        private void OnDestroy()
        {
            _fallRespawner.OnFall -= RespawnPlayer;
        }

        private void RespawnPlayer()
        {
            if (_checkPoint != null) _checkPoint.ReSpawn(_player);
            else _fallbackCheckpoint.ReSpawn(_player);
        }

        public void AssignCheckPoint(CheckPointSaver checkPointSaver)
        {
            _checkPoint = checkPointSaver;
        }
    }  
}