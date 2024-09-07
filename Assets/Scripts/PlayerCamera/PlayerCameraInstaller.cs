using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.PlayerCamera
{
    public class PlayerCameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _cameraInstance;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_cameraInstance);
        }
    }
}