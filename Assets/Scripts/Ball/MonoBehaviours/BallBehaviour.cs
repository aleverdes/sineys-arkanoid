using TaigaGames.SineysArkanoid.Ball.Services;
using TaigaGames.SineysArkanoid.Level.MonoBehaviours;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Ball.MonoBehaviours
{
    public class BallBehaviour : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public CircleCollider2D Collider { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        [Inject] private readonly BallService _ballService;
        
        private void Reset()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Collider = GetComponent<CircleCollider2D>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<DeathZoneBehaviour>(out var deathZoneBehaviour)) 
                _ballService.DestroyBall(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<BlockBehaviour>(out var blockBehaviour)) 
                Destroy(blockBehaviour.gameObject);
        }
    }
}