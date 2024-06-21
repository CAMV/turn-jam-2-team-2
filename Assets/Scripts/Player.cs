using UnityEngine;

namespace TurnJam2
{
    public class Player : MonoBehaviour
    {
        private static readonly int AnimatorSpeedParam = Animator.StringToHash("Speed");

        [Header("Movement")] [SerializeField] private float _speed = 4f;
        [SerializeField] private float _rotationSpeed = 10f;

        private CharacterController _characterController;
        private Animator _animator;

        private Transform _mainCameraTransform;
        private Vector3 _warpPosition;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            _mainCameraTransform = Camera.main.transform;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            var direction = _mainCameraTransform.TransformDirection(input);
            direction.y = 0f;
            direction.Normalize();

            var movement = direction * (_speed * Time.deltaTime);
            movement.y = Physics.gravity.y * Time.deltaTime;
            _characterController.Move(movement);

            _animator.SetFloat(AnimatorSpeedParam, input.magnitude * _speed);

            if (input.magnitude == 0f) return;

            var targetRotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        
        private void LateUpdate()
        {
            if (_warpPosition == Vector3.zero) return;
            _characterController.gameObject.SetActive(false);
            transform.position = _warpPosition;
            _warpPosition = Vector3.zero;
            _characterController.gameObject.SetActive(true);
        }
        
        public void WarpToPosition(Vector3 newPosition)
        {
            _warpPosition = newPosition;
        }
    }
}