using TaigaGames.SineysArkanoid.Pad.ScriptableObjects;
using TaigaGames.SineysArkanoid.Pad.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Pad.Scenarios
{
    public class PadScenario : ITickable
    {
        [Inject] private readonly PadService _padService;
        [Inject] private readonly PadSettings _padSettings;
        [Inject] private readonly Camera _camera;
        
        private bool _isKeyboardInput;
        
        public void Tick()
        {
            UpdateInputWay();
            
            if (!_padService.IsPadCreated)
                return;
            
            if (_isKeyboardInput)
                UpdatePadByKeyboardInput();
            else
                UpdatePadByMouseInput();
        }
        
        private void UpdateInputWay()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.mousePositionDelta.magnitude > 1f)
                _isKeyboardInput = false;
            else if (Input.anyKeyDown)
                _isKeyboardInput = true;
        }
        
        private void UpdatePadByKeyboardInput()
        {
            if (!_padService.TryGetPadPosition(out var padPosition))
                return;
            
            var delta = 0f;
            
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
                delta = -1;
            
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                delta = 1;
            
            if (delta == 0) return;
            
            padPosition.x += delta * Time.deltaTime * _padSettings.KeyboardMovementSpeed;
            _padService.TryMovePad(padPosition.x);
        }
        
        private void UpdatePadByMouseInput()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _padService.TryMovePad(mousePosition.x);
        }
    }
}