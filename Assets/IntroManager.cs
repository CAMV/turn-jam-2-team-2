using UnityEngine;

public class IntroManager : MonoBehaviour
{

    [SerializeField]
    private Material _materialForGears;
    [SerializeField]
    private MeshRenderer[] _gears;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _fakePlayer;

    [SerializeField]
    private GameObject _introVCam, _followVCam;

    public void ChangePlayers()
    {
        _fakePlayer.SetActive(false);
        _player.SetActive(true);

        _introVCam.SetActive(false);
        _followVCam.SetActive(true);
    }

    public void ChangeGearsMaterial()
    {
        foreach (var g in _gears)
        {
            g.material = _materialForGears;
        }
    }
}
