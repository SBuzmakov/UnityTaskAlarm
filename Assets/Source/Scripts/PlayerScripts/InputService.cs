using System;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class InputService : MonoBehaviour
    {
        private const string AxisHorizontalName = "Horizontal";
        private const KeyCode JumpKey = KeyCode.Space;

        public event Action PressedJumpKey;
        public event Action PressedMoveKey;
        public event Action StoppedMove;

        public float Direction { get; private set; }

        private void Update()
        {
            Move();

            UpdateJumpInput();
        }

        private void Move()
        {
            Direction = Input.GetAxis(AxisHorizontalName);

            if (Direction != 0)
                PressedMoveKey?.Invoke();
            else
                StoppedMove?.Invoke();
        }

        private void UpdateJumpInput()
        {
            if (Input.GetKeyDown(JumpKey))
                PressedJumpKey?.Invoke();
        }
    }
}