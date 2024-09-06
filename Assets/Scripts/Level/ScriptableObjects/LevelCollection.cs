using System.Collections.Generic;
using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelCollection", menuName = "SineysArkanoid/LevelCollection")]
    public class LevelCollection : ScriptableObject
    {
        [SerializeField] private LevelDescriptor[] _levels;
        
        public IReadOnlyList<LevelDescriptor> Levels => _levels;
    }
}