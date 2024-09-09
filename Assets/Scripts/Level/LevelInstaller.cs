using TaigaGames.SineysArkanoid.Level.MonoBehaviours;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters;
using TaigaGames.SineysArkanoid.Level.Services;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private MapSettings _mapSettings;
        [SerializeField] private LevelCollection _levelCollection;
        
        [Header("Blocks")]
        [SerializeField] private BlockCollection _blockCollection;
        [SerializeField] private BlockBehaviour _blockPrefab;
        
        [Header("Boosters")]
        [SerializeField] private BoostersCollection _boostersCollection;
        [SerializeField] private BoosterBehaviour _boosterPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_mapSettings);
            Container.BindInstance(_levelCollection);
            
            Container.BindInstance(_blockCollection);
            Container.BindInstance(_blockPrefab);
            
            Container.BindInstance(_boostersCollection);
            Container.BindInstance(_boosterPrefab);

            Container.BindInterfacesAndSelfTo<LevelGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<BlockService>().AsSingle();
        }
    }
}