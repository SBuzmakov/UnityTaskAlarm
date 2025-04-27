using UnityEngine;

namespace Source.Scripts
{
    public class InputService : MonoBehaviour
    {
        private const string AxisHorizontalName = "Horizontal";
        
        public float Direction { get; private set; }

        private void Update()
        {
            Direction = Input.GetAxis(AxisHorizontalName);
        }
    }
}