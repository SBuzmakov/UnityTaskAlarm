using Source.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.Pool;

namespace Source.Scripts.LootScripts
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private Loot _lootPrefab;
        [SerializeField] private Collider2D _lootZoneCollider;
        [SerializeField] private Collector _collector;

        private ObjectPool<Loot> _pool;
        private LootFactory _lootFactory;

        public void Awake()
        {
            _lootFactory = new LootFactory(_lootPrefab);

            _pool = new ObjectPool<Loot>(
                createFunc: CreateLoot,
                actionOnGet: OnGetLoot,
                actionOnRelease: SpawnLoot
            );

            SpawnLoot(CreateLoot());
        }

        private void OnEnable()
        {
            _collector.CollectedLoot += ReleaseLoot;
        }

        private void OnDisable()
        {
            _collector.CollectedLoot -= ReleaseLoot;
        }

        private Loot CreateLoot()
        {
            Loot newLoot = _lootFactory.Create();
            newLoot.Destroyed += Dispose;

            return newLoot;
        }

        private void Dispose(Loot loot)
        {
            loot.Destroyed -= Dispose;
        }

        private void SpawnLoot(Loot loot)
        {
            loot = _pool.Get();
            OnGetLoot(loot);
        }

        private void OnGetLoot(Loot loot)
        {
            loot.gameObject.SetActive(true);
            loot.transform.position = GetLootPosition();
        }

        private Vector2 GetLootPosition()
        {
            float halfDivider = 2.0f;

            Vector2 size = _lootZoneCollider.bounds.size;
            Vector2 center = _lootZoneCollider.bounds.center;

            float x = Random.Range(center.x - size.x / halfDivider, center.x + size.x / halfDivider);
            float y = Random.Range(center.y - size.y / halfDivider, center.y + size.y / halfDivider);

            return new Vector2(x, y);
        }

        private void ReleaseLoot(Loot loot)
        {
            _pool.Release(loot);
            loot.gameObject.SetActive(false);
        }
    }
}