using System;
using Source.Scripts.LootScripts;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class Collector : MonoBehaviour
    {
        public event Action<Loot> CollectedLoot;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Loot loot))
            {
                CollectedLoot?.Invoke(loot);
            }
        }
    }
}
