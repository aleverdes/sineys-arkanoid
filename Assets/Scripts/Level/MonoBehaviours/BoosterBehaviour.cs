using System;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters;
using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Level.MonoBehaviours
{
    public class BoosterBehaviour : MonoBehaviour
    {
        [field: SerializeField] public float Gravity = -5f;
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        
        public BoosterDescriptor Booster { get; private set; }
        
        [Inject] private readonly SessionService _sessionService;

        private void FixedUpdate()
        {
            Rigidbody2D.linearVelocity += Vector2.up * Gravity * Time.fixedDeltaTime;
        }

        [Inject]
        private void OnInitialize()
        {
            _sessionService.LifeCountChanged += OnLifeCountChanged;
        }

        private void OnDestroy()
        {
            _sessionService.LifeCountChanged -= OnLifeCountChanged;
        }

        private void OnLifeCountChanged(int life)
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (!_sessionService.IsInProcess())
                Destroy(gameObject);
        }

        public void Setup(BoosterDescriptor booster)
        {
            Booster = booster;
            SpriteRenderer.sprite = booster.Sprite;
        }
    }
}