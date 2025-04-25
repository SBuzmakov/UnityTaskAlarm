using UnityEngine;

namespace Source.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private InputService _inputService;
        [SerializeField] private float _speed;

        private void OnEnable()
        {
            _inputService.PressedMoveButton += Move;
        }

        private void OnDisable()
        {
            _inputService.PressedMoveButton -= Move;
        }
        
        private void Move(float direction)
        {
            transform.Translate(Vector3.right * (_speed * Time.deltaTime *  direction));
        }
    }
}

