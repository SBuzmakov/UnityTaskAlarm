using System;
using UnityEngine;

namespace Source.Scripts.LootScripts
{
    public class Loot : MonoBehaviour
    {
        public event Action<Loot> Destroyed;

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}