using TaigaGames.SineysArkanoid.Ball.Services;
using TaigaGames.SineysArkanoid.Level.MonoBehaviours;
using TaigaGames.SineysArkanoid.Level.Services;
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
        [Inject] private readonly BallSpeedService _ballSpeedService;
        [Inject] private readonly BlockService _blockService;

        private bool _prepared;
        
        [Inject]
        public void Prepare()
        {
            _prepared = true;
        }
        
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
            if (!_prepared)
                return;
            
            if (other.gameObject.TryGetComponent<BlockBehaviour>(out var blockBehaviour)) 
                _blockService.DestroyBlock(blockBehaviour);

            // Forbid horizontal movement
            const float minNormalY = 0.2f;
            if (Mathf.Abs(other.contacts[0].normal.normalized.y) < minNormalY)
            {
                var velocity = Rigidbody.linearVelocity;
                velocity = velocity.magnitude * new Vector2(velocity.normalized.x, minNormalY * Mathf.Sign(velocity.y)).normalized;
                Rigidbody.linearVelocity = velocity;
            }
            
            Rigidbody.linearVelocity = Rigidbody.linearVelocity.normalized * _ballSpeedService.Speed;
        }
    }
}