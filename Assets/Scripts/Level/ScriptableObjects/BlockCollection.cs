using System.Collections.Generic;
using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BlockCollection", menuName = "SineysArkanoid/BlockCollection")]
    public class BlockCollection : ScriptableObject
    {
        [SerializeField] private BlockDescriptor[] _blocks;
        
        public IReadOnlyList<BlockDescriptor> Blocks => _blocks;
    }
}