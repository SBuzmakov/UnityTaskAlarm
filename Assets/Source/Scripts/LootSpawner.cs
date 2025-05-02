using UnityEngine;
using UnityEngine.Pool;

namespace Source.Scripts
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private Loot _lootPrefab;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Alarm _alarm;
        
        private ObjectPool<Loot> _pool;
        private LootFactory _lootFactory;

        public void Awake()
        {
            _lootFactory = new LootFactory(_lootPrefab);

            _pool = new ObjectPool<Loot>(
                createFunc: CreateLoot,
                actionOnGet: OnGetLoot
            );

            SpawnLoot();
        }

        private void OnEnable()
        {
            _alarm.Finished += SpawnLoot;
        }

        private void OnDisable()
        {
            _alarm.Finished -= SpawnLoot;
        }

        private Loot CreateLoot()
        {
            Loot newLoot = _lootFactory.Create();
            newLoot.PlayerEntered += ReleaseLoot;
            newLoot.Destroyed += Dispose;

            return newLoot;
        }

        private void Dispose(Loot loot)
        {
            loot.Destroyed -= Dispose;
            loot.PlayerEntered -= ReleaseLoot;
        }

        private void SpawnLoot()
        {
            Loot loot = _pool.Get();
            OnGetLoot(loot);
        }

        private void OnGetLoot(Loot loot)
        {
            loot.gameObject.SetActive(true);
            loot.transform.position = _spawnPosition.position;
        }

        private void ReleaseLoot(Loot loot)
        {
            _pool.Release(loot);
            loot.gameObject.SetActive(false);
        }
    }
}