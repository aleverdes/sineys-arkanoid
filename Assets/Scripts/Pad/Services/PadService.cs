using TaigaGames.SineysArkanoid.Pad.MonoBehaviours;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Pad.Services
{
    public class PadService
    {
        [Inject] private readonly PadArea _padArea;
        [Inject] private readonly PadBehaviour _padBehaviourPrefab;
        
        private PadBehaviour _padBehaviourInstance;
        
        /// <summary>
        /// Is pad created
        /// </summary>
        public bool IsPadCreated => _padBehaviourInstance != null;
        
        /// <summary>
        /// Create pad
        /// </summary>
        /// <exception cref="Exception">Pad already created</exception>
        public void CreatePad()
        {
            if (_padBehaviourInstance != null)
                throw new System.Exception("Pad already created");
            
            _padBehaviourInstance = Object.Instantiate(_padBehaviourPrefab, _padArea.transform);
        }
        
        /// <summary>
        /// Destroy pad
        /// </summary>
        /// <exception cref="Exception">Pad not created</exception>
        public void DestroyPad()
        {
            if (_padBehaviourInstance == null)
                throw new System.Exception("Pad not created");
            
            Object.Destroy(_padBehaviourInstance.gameObject);
            _padBehaviourInstance = null;
        }
        
        /// <summary>
        /// Move pad to the specified position
        /// </summary>
        /// <param name="x">Position to move to (in units)</param>
        /// <returns>True if the pad was moved, false if the pad is not created</returns>
        public bool TryMovePad(float x)
        {
            if (_padBehaviourInstance == null)
                return false;
            
            var spriteSize = _padBehaviourInstance.SpriteRenderer.size.x;
            var halfSpriteSize = spriteSize / 2;
            var scaledHalfSpriteSize = halfSpriteSize * _padBehaviourInstance.transform.localScale.x;
            var position = Mathf.Clamp(x, -_padArea.Width / 2 + scaledHalfSpriteSize, _padArea.Width / 2 - scaledHalfSpriteSize);
            _padBehaviourInstance.transform.position = new Vector3(position, _padBehaviourInstance.transform.position.y, _padBehaviourInstance.transform.position.z);
            return true;
        }
        
        /// <summary>
        /// Get pad position
        /// </summary>
        /// <param name="padPosition">Pad position (in units)</param>
        /// <returns>True if the pad is created, false otherwise</returns>
        public bool TryGetPadPositionX(out float padPosition)
        {
            if (_padBehaviourInstance == null)
            {
                padPosition = 0;
                return false;
            }
            
            padPosition = _padBehaviourInstance.transform.position.x;
            return true;
        }
    }
}