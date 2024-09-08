using TaigaGames.SineysArkanoid.Pad.MonoBehaviours;
using TaigaGames.SineysArkanoid.Pad.Scenarios;
using TaigaGames.SineysArkanoid.Pad.ScriptableObjects;
using TaigaGames.SineysArkanoid.Pad.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Pad
{
    public class PadInstaller : MonoInstaller
    {
        [SerializeField] private PadBehaviour _padBehaviourPrefab;
        [SerializeField] private PadArea _padAreaInstance;
        [SerializeField] private PadSettings _padSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_padBehaviourPrefab);
            Container.BindInstance(_padAreaInstance);
            Container.BindInstance(_padSettings);

            Container.BindInterfacesAndSelfTo<PadService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PadLaunchService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PadScenario>().AsSingle();
        }
    }
}