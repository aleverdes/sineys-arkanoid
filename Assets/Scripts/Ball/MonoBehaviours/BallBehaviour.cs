using System;
using TaigaGames.SineysArkanoid.Pad.MonoBehaviours;
using TaigaGames.SineysArkanoid.Pad.ScriptableObjects;
using TaigaGames.SineysArkanoid.Pad.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Ball.MonoBehaviours
{
    public class BallBehaviour : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public CircleCollider2D Collider { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        
        private void Reset()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Collider = GetComponent<CircleCollider2D>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (Rigidbody.linearVelocity.magnitude < 1f)
            {
                Rigidbody.linearVelocity = Rigidbody.linearVelocity.normalized * 1f;
            }
        }
    }
}