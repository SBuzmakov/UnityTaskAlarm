using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Loot : MonoBehaviour
    {
        public event Action<Loot> PickedUp;
        public event Action<Loot> Destroyed;
        public event Action NeededSpawn;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player _))
            {
                PickedUp?.Invoke(this);
                NeededSpawn?.Invoke();
            }
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}