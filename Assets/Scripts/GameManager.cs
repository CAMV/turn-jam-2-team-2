using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private void Awake()
    {
        Locator.ProvideGameManager(this);
    }
}