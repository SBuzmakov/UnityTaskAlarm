using System;
using Source.Scripts.LootScripts;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public event Action<Loot> CollectedLoot;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Loot loot))
            {
                CollectedLoot?.Invoke(loot);
            }
        }
    }
}
