using TaigaGames.SineysArkanoid.Level.MonoBehaviours;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects;
using TaigaGames.SineysArkanoid.Level.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private MapSettings _mapSettings;
        [SerializeField] private LevelCollection _levelCollection;
        [SerializeField] private BlockCollection _blockCollection;
        [SerializeField] private BlockBehaviour _blockPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_mapSettings);
            Container.BindInstance(_levelCollection);
            Container.BindInstance(_blockCollection);
            Container.BindInstance(_blockPrefab);

            Container.BindInterfacesAndSelfTo<LevelGenerator>().AsSingle();
        }
    }
}