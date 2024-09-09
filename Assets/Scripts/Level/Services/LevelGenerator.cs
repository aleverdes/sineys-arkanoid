using System;
using TaigaGames.SineysArkanoid.Level.MonoBehaviours;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects;
using TaigaGames.SineysArkanoid.Utils;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace TaigaGames.SineysArkanoid.Level.Services
{
    public class LevelGenerator : IDisposable
    {
        private const float BlockSelectionRandom = 0.1f;
        
        [Inject] private readonly MapSettings _mapSettings;
        [Inject] private readonly BlockCollection _blockCollection;
        [Inject] private readonly BlockBehaviour _blockPrefab;
        [Inject] private readonly BlockService _blockService;
        
        private Transform _blocksParent;

        public void Initialize()
        {
            _blocksParent = new GameObject("Blocks").transform;
            _blocksParent.position = new Vector3(_mapSettings.Center.x, _mapSettings.Center.y, _mapSettings.ZIndex);
        }

        public void Dispose()
        {
            if (_blocksParent)
                Object.Destroy(_blocksParent.gameObject);
        }

        public void Clear()
        {
            for (var i = _blocksParent.childCount - 1; i >= 0; i--) 
                Object.Destroy(_blocksParent.GetChild(i).gameObject);
            _blockService.Reset();
        }
        
        public void GenerateLevel(LevelDescriptor levelDescriptor)
        {
            Clear();
            
            var mapSizeInBlocks = new Vector2Int(
                Mathf.FloorToInt(_mapSettings.MapSize.x * levelDescriptor.Size.x / _mapSettings.BlockSize.x), 
                Mathf.FloorToInt(_mapSettings.MapSize.y * levelDescriptor.Size.y / _mapSettings.BlockSize.y)
            );
            
            var mapPrototype = TextureScaler.Scale(levelDescriptor.LevelTexture, mapSizeInBlocks.x, mapSizeInBlocks.y, FilterMode.Point);
            for (var x = 0; x < mapSizeInBlocks.x; x++)
            for (var y = 0; y < mapSizeInBlocks.y; y++)
            {
                var color = mapPrototype.GetPixel(x, y);
                if (color.a < 0.5f) continue;
                
                var descriptor = GetBlockDescriptor(color);
                _blockService.CreateBlock(descriptor, new Vector2Int(x, y), _blocksParent);
            }
        }
        
        private BlockDescriptor GetBlockDescriptor(Color color)
        {
            var bestDistance = float.MaxValue;
            BlockDescriptor bestDescriptor = null;
            
            foreach (var blockDescriptor in _blockCollection.Blocks)
            {
                var distance = ColorDistance(blockDescriptor.EditorColor, color) * Random.Range(1f - BlockSelectionRandom, 1f + BlockSelectionRandom);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestDescriptor = blockDescriptor;
                }
            }
            
            return bestDescriptor;
            
            float ColorDistance(Color c1, Color c2)
            {
                var rmean = (c1.r + c2.r) / 2;
                var r = c1.r - c2.r;
                var g = c1.g - c2.g;
                var b = c1.b - c2.b;
                return Mathf.Sqrt((2 + rmean) * r * r + 4 * g * g + (3 - rmean) * b * b);
            }
        }
    }
}