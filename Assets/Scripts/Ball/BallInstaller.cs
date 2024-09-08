using TaigaGames.SineysArkanoid.Ball.MonoBehaviours;
using TaigaGames.SineysArkanoid.Ball.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Ball
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private BallBehaviour _ballPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_ballPrefab);
            
            Container.BindInterfacesAndSelfTo<BallService>().AsSingle();
        }
    }
}