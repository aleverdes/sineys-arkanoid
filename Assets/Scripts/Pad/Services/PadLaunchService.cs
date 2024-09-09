using TaigaGames.SineysArkanoid.Ball.MonoBehaviours;
using TaigaGames.SineysArkanoid.Ball.Services;
using TaigaGames.SineysArkanoid.Pad.ScriptableObjects;
using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Pad.Services
{
    public class PadLaunchService : ITickable
    {
        [Inject] private readonly PadService _padService;
        [Inject] private readonly PadSettings _padSettings;
        [Inject] private readonly BallService _ballService;
        [Inject] private readonly BallSpeedService _ballSpeedService;
        [Inject] private readonly SessionService _sessionService;
        
        private BallBehaviour _ballForLaunch;
        private Vector2 _prevPadPosition;
        
        public void SetBallForLaunch(BallBehaviour ballBehaviour)
        {
            if (!_padService.IsPadCreated)
                return;

            var pad = _padService.PadBehaviourInstance;
            var padHeight = pad.transform.localScale.y * pad.SpriteRenderer.size.y / 2f;
            var ballRadius = ballBehaviour.transform.localScale.y * ballBehaviour.SpriteRenderer.size.y / 2;
            
            _ballService.FreezeBall(ballBehaviour);
            _ballService.SetBallPosition(ballBehaviour, pad.transform.position + Vector3.up * (padHeight + ballRadius));
            
            _ballForLaunch = ballBehaviour;
            _prevPadPosition = pad.transform.position;
        }

        public void Tick()
        {
            if (!_padService.IsPadCreated || !_sessionService.IsInProcess() || Time.timeScale < 0.5f)
                return;

            MoveBall();

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                LaunchBall();
        }

        private void MoveBall()
        {
            if (_ballForLaunch == null || !_padService.IsPadCreated)
                return;

            var pad = _padService.PadBehaviourInstance;
            var padPosition = pad.transform.position;
            var delta = padPosition.x - _prevPadPosition.x;
            delta *= 0.1f;
            
            var ballPosition = _ballForLaunch.transform.position;
            var relativePosition = ballPosition - padPosition;
            
            var padWidth = pad.transform.localScale.x * pad.SpriteRenderer.size.x;
            relativePosition.x -= delta;
            relativePosition.x = Mathf.Clamp(relativePosition.x, -padWidth / 2f, padWidth / 2f);
            
            _ballService.SetBallPosition(_ballForLaunch, padPosition + relativePosition);
            _prevPadPosition = padPosition;
        }
        
        public void LaunchBall()
        {
            if (!_padService.IsPadCreated || !_ballForLaunch)
                return;

            _ballService.UnfreezeBall(_ballForLaunch);

            var forceAngle = Mathf.LerpAngle(-_padSettings.MaxLaunchAngle, _padSettings.MaxLaunchAngle, GetBallToPadRatio(_ballForLaunch));
            var vector = Quaternion.Euler(0f, 0f, forceAngle) * Vector2.up;
            
            _ballService.AddForceToBall(_ballForLaunch, _ballSpeedService.Speed * vector);

            _ballForLaunch = null;
        }

        public float GetBallToPadRatio(BallBehaviour ballBehaviour)
        {
            if (ballBehaviour == null || !_padService.IsPadCreated)
                return 0f;

            var pad = _padService.PadBehaviourInstance;
            var padPosition = pad.transform.position;
            
            var ballPosition = ballBehaviour.transform.position;
            
            var relativePosition = ballPosition - padPosition;
            
            var padWidth = pad.transform.localScale.x * pad.SpriteRenderer.size.x;
            var ratio = Mathf.InverseLerp(-padWidth / 2f, padWidth / 2f, relativePosition.x);

            return 1f - ratio;
        }
    }
}