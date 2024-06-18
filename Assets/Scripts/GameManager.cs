using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private Player _player;
    [SerializeField]
    private float _speed = 1;
    
    private Action<Vector3, Light> UpdateGlassLights;

    public void SubscribeGlassLight(Action<Vector3, Light> update)
    {
        UpdateGlassLights += update;
    }

    void Awake()
    {
        Locator.ProvideGameManager(this);
    }

    void Update()
    {
        var forward = new Vector3(
            Camera.main.transform.forward.x, 
            0,
            Camera.main.transform.forward.z).normalized;

        var right = Vector3.Cross(Vector3.up, forward);

        _player.transform.position += right * Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        _player.transform.position += forward * Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        if (UpdateGlassLights != null)
            UpdateGlassLights(_player.transform.position, _player.Light);
    }
}
