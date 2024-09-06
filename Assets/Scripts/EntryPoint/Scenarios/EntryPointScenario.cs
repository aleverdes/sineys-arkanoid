using TaigaGames.SineysArkanoid.Level.ScriptableObjects;
using TaigaGames.SineysArkanoid.Level.Services;
using Zenject;

namespace TaigaGames.SineysArkanoid.EntryPoint.Scenarios
{
    public class EntryPointScenario : IInitializable
    {
        [Inject] private readonly LevelGenerator _levelGenerator;
        [Inject] private readonly LevelCollection _levelCollection;
        
        public void Initialize()
        {
            _levelGenerator.Initialize();
            _levelGenerator.GenerateLevel(_levelCollection.Levels[0]);
        }
    }
}