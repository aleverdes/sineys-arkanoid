using System.Linq;
using TaigaGames.SineysArkanoid.Ball.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters
{
    [CreateAssetMenu(fileName = "DoubleBallBooster", menuName = "SineysArkanoid/Boosters/DoubleBallBooster")]
    public class DoubleBallBooster : BoosterDescriptor
    {
        public override void Execute(DiContainer container)
        {
            var ballService = container.Resolve<BallService>();
            var ballSpeedService = container.Resolve<BallSpeedService>();
            
            var list = ballService.GetBalls().ToArray();
            
            foreach (var ball in list)
            {
                var newBall = ballService.CreateBall(ball.transform.position + 0.1f * Vector3.right, false);
                newBall.Rigidbody.linearVelocity = ballSpeedService.Speed * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            }
        }
    }
}