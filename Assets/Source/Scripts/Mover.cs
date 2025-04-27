using UnityEngine;

namespace Source.Scripts
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private InputService _inputService;
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.Translate(Vector3.right * (_speed * Time.deltaTime *  _inputService.Direction));
        }
    }
}

