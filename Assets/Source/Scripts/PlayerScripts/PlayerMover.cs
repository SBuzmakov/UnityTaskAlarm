using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private InputService _inputService;
        [SerializeField] private Foot _foot;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private PlayerAnimator _playerAnimator;

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

            if (_isFacingRight)
                transform.Translate(Vector3.right * (_speed * Time.deltaTime * _inputService.Direction));
            else
                transform.Translate(Vector3.left * (_speed * Time.deltaTime * _inputService.Direction));

            if (_inputService.Direction == 0f)
                _playerAnimator.PlayIdleClip();
            else
                _playerAnimator.PlayWalkClip();
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
            transform.Rotate(0f, 180f, 0f);
        }

        private void Jump()
        {
            if (_foot.IsGrounded)
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            _needToJump = false;
        }
    }
}