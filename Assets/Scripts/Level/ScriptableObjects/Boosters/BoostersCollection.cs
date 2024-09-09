using System.Collections.Generic;
using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters
{
    [CreateAssetMenu(fileName = "BoostersCollection", menuName = "SineysArkanoid/BoostersCollection")]
    public class BoostersCollection : ScriptableObject
    {
        [SerializeField] private BoosterDescriptor[] _boosters;
        
        public IReadOnlyList<BoosterDescriptor> Boosters => _boosters;
    }
}