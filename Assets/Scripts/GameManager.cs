using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private Action<Vector3, Light> _updateGlassLights;
    
    private void Awake()
    {
        Locator.ProvideGameManager(this);
    }

    private void Update()
    {
        _updateGlassLights?.Invoke(_player.transform.position, _player.Light);
    }
    
    public void SubscribeGlassLight(Action<Vector3, Light> update)
    {
        _updateGlassLights += update;
    }
}