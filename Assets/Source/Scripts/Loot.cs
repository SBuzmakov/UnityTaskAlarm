using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Loot : MonoBehaviour
    {
        public event Action<Loot> PlayerEntered;
        public event Action<Loot> Destroyed;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player _))
                PlayerEntered?.Invoke(this);
        }
        
        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
