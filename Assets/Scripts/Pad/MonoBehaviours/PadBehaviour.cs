using System;
using TaigaGames.SineysArkanoid.Ball.MonoBehaviours;
using TaigaGames.SineysArkanoid.Ball.Services;
using TaigaGames.SineysArkanoid.Level.MonoBehaviours;
using TaigaGames.SineysArkanoid.Pad.ScriptableObjects;
using TaigaGames.SineysArkanoid.Pad.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Pad.MonoBehaviours
{
    public class PadBehaviour : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public BoxCollider2D Collider { get; private set; }
        
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly PadLaunchService _padLaunchService;
        [Inject] private readonly PadSettings _padSettings;
        [Inject] private readonly BallService _ballService;
        [Inject] private readonly BallSpeedService _ballSpeedService;

        private float _collisionCooldown;

        private void Update()
        {
            _collisionCooldown -= Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collisionCooldown > 0f)
                return;
            
            if (other.gameObject.TryGetComponent<BallBehaviour>(out var ballBehaviour))
            {
                var ratio = _padLaunchService.GetBallToPadRatio(ballBehaviour);
                var forceAngle = Mathf.LerpAngle(-_padSettings.MaxLaunchAngle, _padSettings.MaxLaunchAngle, ratio);
                var vector = Quaternion.Euler(0f, 0f, forceAngle) * Vector2.up;
                _ballService.SetForceToBall(ballBehaviour, _ballSpeedService.Speed * vector);
            }

            _collisionCooldown = 0.2f;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BoosterBehaviour boosterBehaviour))
            {
                boosterBehaviour.Booster.Execute(_diContainer);
                Destroy(boosterBehaviour.gameObject);
            }
        }
    }
}