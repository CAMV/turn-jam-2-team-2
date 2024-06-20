using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Light _pointLight;
    
    private Camera _mainCamera;

    public Light Light => _pointLight;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        var forward = _mainCamera.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        var right = Vector3.Cross(Vector3.up, forward);

        transform.position += right * (Input.GetAxis("Horizontal") * _speed * Time.deltaTime);
        transform.position += forward * (Input.GetAxis("Vertical") * _speed * Time.deltaTime);
    }
}