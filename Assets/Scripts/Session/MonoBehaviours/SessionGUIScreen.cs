using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.MonoBehaviours
{
    public class SessionGUIScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform _lifeRoot;
        [SerializeField] private GameObject _lifePrefab;
        
        [Inject] private readonly SessionService _sessionService;

        private void OnEnable()
        {
            _sessionService.LifeCountChanged += OnLifeCountChanged;
            OnLifeCountChanged(_sessionService.LifeCount);
        }
        
        private void OnDisable()
        {
            _sessionService.LifeCountChanged -= OnLifeCountChanged;
        }

        private void OnLifeCountChanged(int lifeCount)
        {
            for (var i = _lifeRoot.childCount - 1; i >= 0; i--) 
                Destroy(_lifeRoot.GetChild(i).gameObject);
            for (var i = 0; i < lifeCount; i++) 
                Instantiate(_lifePrefab, _lifeRoot);
        }
    }
}