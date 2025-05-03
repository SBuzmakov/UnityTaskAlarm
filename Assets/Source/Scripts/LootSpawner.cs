using UnityEngine;
using UnityEngine.Pool;

namespace Source.Scripts
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private Loot _lootPrefab;
        [SerializeField] private Collider2D _lootCollider;
        
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

        private Loot CreateLoot()
        {
            Loot newLoot = _lootFactory.Create();
            newLoot.PickedUp += ReleaseLoot;
            newLoot.NeededSpawn += SpawnLoot;
            newLoot.Destroyed += Dispose;

            return newLoot;
        }

        private void Dispose(Loot loot)
        {
            loot.Destroyed -= Dispose;
            loot.PickedUp -= ReleaseLoot;
            loot.NeededSpawn -= SpawnLoot;
        }

        private void SpawnLoot()
        {
            Loot loot = _pool.Get();
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
            
            Vector2 size = _lootCollider.bounds.size;
            Vector2 center = _lootCollider.bounds.center;
            
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