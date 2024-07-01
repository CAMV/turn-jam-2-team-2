using UnityEngine;

namespace TurnJam2
{
    public class teleporte : MonoBehaviour
    {
        [SerializeField]
        Transform _tpPoint;
        [SerializeField]
        GameObject _oldCamera;
        [SerializeField]
        GameObject _newCamera;
        [SerializeField]
        Transform _player;

        private void Update()
        {
            if (Vector3.Distance(transform.position, _player.position) < 2.5)
            {
                Locator.GameManager.Player.WarpToPosition(_tpPoint.position);
                _oldCamera.SetActive(false);
                _newCamera.SetActive(true);
            }
        }
    }

}
