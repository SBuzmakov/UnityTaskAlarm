using UnityEngine;

namespace Source.Scripts
{
    public class ParallaxMover : MonoBehaviour
    {
        [SerializeField] private InputService _inputService;
        [SerializeField] private float _speed;

        private void OnEnable()
        {
            _inputService.PressedMoveKey += Move;
        }

        private void OnDisable()
        {
            _inputService.PressedMoveKey -= Move;
        }
        
        private void Move()
        {
                transform.Translate(Vector3.right * (_inputService.Direction * _speed * Time.deltaTime));
        }
    }
}