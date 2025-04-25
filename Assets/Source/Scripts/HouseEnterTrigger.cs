using UnityEngine;

namespace Source.Scripts
{
    public class HouseEnterTrigger : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _houseWall;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                _houseWall.enabled = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out _))
                _houseWall.enabled = true;
        }
    }
}