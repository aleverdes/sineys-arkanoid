using System.Collections.Generic;
using TaigaGames.SineysArkanoid.Ball.MonoBehaviours;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Ball.Services
{
    public class BallService
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly BallBehaviour _ballPrefab;
        
        private readonly List<BallBehaviour> _balls = new List<BallBehaviour>();
        
        public BallBehaviour CreateBall(Vector2 position, bool frozen = false)
        {
            var ball = _diContainer.InstantiatePrefabForComponent<BallBehaviour>(_ballPrefab);
            ball.transform.position = position;
            ball.Rigidbody.bodyType = frozen ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
            _balls.Add(ball);
            return ball;
        }
        
        public void DestroyBall(BallBehaviour ball)
        {
            Object.Destroy(ball.gameObject);
            _balls.Remove(ball);
        }
        
        public void DestroyAllBalls()
        {
            foreach (var ball in _balls)
                Object.Destroy(ball.gameObject);
            _balls.Clear();
        }
        
        public void FreezeBall(BallBehaviour ball)
        {
            ball.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
        
        public void FreezeAllBalls()
        {
            foreach (var ball in _balls)
                ball.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
        
        public void UnfreezeBall(BallBehaviour ball)
        {
            ball.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        
        public void UnfreezeAllBalls()
        {
            foreach (var ball in _balls)
                ball.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        
        public void AddForceToBall(BallBehaviour ball, Vector2 force)
        {
            ball.Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
        
        public void SetForceToBall(BallBehaviour ball, Vector2 force)
        {
            ball.Rigidbody.linearVelocity = Vector2.zero;
            AddForceToBall(ball, force);
        }
        
        public void SetBallPosition(BallBehaviour ball, Vector2 position)
        {
            ball.transform.position = position;
        }
    }
}