using System;
using UnityEngine;

namespace Source.Scripts
{
    public class InputService : MonoBehaviour
    {
        public event Action<float> PressedMoveButton;

        void Update()
        {
            if (IsMoveButtonPressed(out float direction))
                PressedMoveButton?.Invoke(direction);
        }

        private float OnMoveButtonPressed()
        {
            return Input.GetAxis("Horizontal");
        }

        private bool IsMoveButtonPressed(out float direction)
        {
            direction = OnMoveButtonPressed();
            
            return direction != 0;
        }
    }
}