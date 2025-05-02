using UnityEngine;

namespace Source.Scripts
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private InputService _inputService;
        [SerializeField] private Foots _foots;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;

        private bool _isFacingRight = true;
        private bool _needToJump;

        private void OnEnable()
        {
            _inputService.PressedJumpKey += OnJump;
        }

        private void OnJump()
        {
            _needToJump = true;
        }

        private void Update()
        {
            Move();
        }

        private void FixedUpdate()
        {
            if (_needToJump)
                Jump();
        }

        private void OnDisable()
        {
            _inputService.PressedJumpKey -= OnJump;
        }

        private void Move()
        {
            ChangeFacing();

            transform.Translate(Vector3.right * (_speed * Time.deltaTime * _inputService.Direction));
        }

        private void ChangeFacing()
        {
            if (_inputService.Direction < 0f && _isFacingRight ||
                _inputService.Direction > 0f && _isFacingRight == false)
            {
                Flip();

                _isFacingRight = !_isFacingRight;
            }
        }

        private void Flip()
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        private void Jump()
        {
            if (_foots.IsGrounded)
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            
            _needToJump = false;
        }
    }
}