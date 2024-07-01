using TurnJam2;
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

    [SerializeField] 
    private string _hitRobotSoundName;
    [SerializeField] 
    private string _introSoundName;
    [SerializeField] 
    private string _backgroundSoundName;

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

    public void PlayHitRobot()
    {
        Locator.AudioHandler.Play(_hitRobotSoundName, true);
    }

    public void PlayIntroSoundName()
    {
        Locator.AudioHandler.Play(_introSoundName, true);
    }
    
    public void PlayBackgroundMusic()
    {
        Locator.AudioHandler.Play(_backgroundSoundName, false);
    }
}
