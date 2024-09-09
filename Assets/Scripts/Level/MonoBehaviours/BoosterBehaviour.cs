using System;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters;
using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.MonoBehaviours
{
    public class BoosterBehaviour : MonoBehaviour
    {
        [field: SerializeField] public float Gravity = -5f;
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        
        public BoosterDescriptor Booster { get; private set; }

        private void FixedUpdate()
        {
            Rigidbody2D.linearVelocity += Vector2.up * Gravity * Time.fixedDeltaTime;
        }

        public void Setup(BoosterDescriptor booster)
        {
            Booster = booster;
            SpriteRenderer.sprite = booster.Sprite;
        }
    }
}