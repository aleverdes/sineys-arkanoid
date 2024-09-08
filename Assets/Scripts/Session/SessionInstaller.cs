using TaigaGames.SineysArkanoid.Session.MonoBehaviours;
using TaigaGames.SineysArkanoid.Session.Scenarios;
using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session
{
    public class SessionInstaller : MonoInstaller
    {
        [SerializeField] private SessionFailScreen _sessionFailScreen;
        [SerializeField] private SessionPauseScreen _sessionPauseScreen;
        [SerializeField] private SessionGUIScreen _sessionGUIScreen;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_sessionFailScreen).AsSingle();
            Container.BindInstance(_sessionPauseScreen).AsSingle();
            Container.BindInstance(_sessionGUIScreen).AsSingle();
            
            Container.BindInterfacesAndSelfTo<SessionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SessionUIService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<SessionPauseScenario>().AsSingle();
            Container.BindInterfacesAndSelfTo<SessionGUIScenario>().AsSingle();
        }
    }
}