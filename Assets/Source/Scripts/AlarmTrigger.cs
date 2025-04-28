using System;
using UnityEngine;

namespace Source.Scripts
{
    public class AlarmTrigger : MonoBehaviour
    {
        public event Action Entered;
        public event Action Exited;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
                Entered?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
                Exited?.Invoke();
        }
    }
}