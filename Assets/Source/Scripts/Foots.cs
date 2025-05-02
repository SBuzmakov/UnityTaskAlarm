using UnityEngine;

namespace Source.Scripts
{
    public class Foots : MonoBehaviour
    {
        public bool IsGrounded { get; private set; }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Platform _))
                IsGrounded = true;
        }
        
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Platform _))
                IsGrounded = false;
        }
    }
}
