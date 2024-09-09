using TaigaGames.SineysArkanoid.MainMenu.MonoBehaviours;
using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuScreen _mainMenuScreenPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_mainMenuScreenPrefab).AsSingle();
        }
    }
}