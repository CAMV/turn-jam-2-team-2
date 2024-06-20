using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int AnimatorSpeedParam = Animator.StringToHash("Speed");
    
    [Header("Movement")]
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _rotationSpeed = 10f;
    
    private Transform _mainCameraTransform;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        var forward = _mainCameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        var right = Vector3.Cross(Vector3.up, forward);

        var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        transform.position += right * (input.x * _speed * Time.deltaTime);
        transform.position += forward * (input.z * _speed * Time.deltaTime);

        _animator.SetFloat(AnimatorSpeedParam, input.magnitude * _speed);

        if (input.magnitude == 0f) return;
        
        var direction = _mainCameraTransform.TransformDirection(input);
        direction.y = 0f;
        direction.Normalize();
        
        var targetRotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}