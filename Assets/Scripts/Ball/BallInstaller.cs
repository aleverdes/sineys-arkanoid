using TaigaGames.SineysArkanoid.Ball.MonoBehaviours;
using TaigaGames.SineysArkanoid.Ball.ScriptableObjects;
using TaigaGames.SineysArkanoid.Ball.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Ball
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private BallBehaviour _ballPrefab;
        [SerializeField] private BallSettings _ballSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_ballPrefab);
            Container.BindInstance(_ballSettings);
            
            Container.BindInterfacesAndSelfTo<BallService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BallSpeedService>().AsSingle();
        }
    }
}