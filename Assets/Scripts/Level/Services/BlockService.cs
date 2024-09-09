using System;
using System.Collections.Generic;
using TaigaGames.SineysArkanoid.Ball.Services;
using TaigaGames.SineysArkanoid.Level.MonoBehaviours;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects;
using TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace TaigaGames.SineysArkanoid.Level.Services
{
    public class BlockService
    {
        private const float BoosterChance = 0.05f;
        private const int MaxBallsCount = 10;

        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly MapSettings _mapSettings;
        [Inject] private readonly BlockCollection _blockCollection;
        [Inject] private readonly BlockBehaviour _blockPrefab;
        [Inject] private readonly BoosterBehaviour _boosterPrefab;
        [Inject] private readonly BoostersCollection _boostersCollection;
        [Inject] private readonly BallService _ballService;
        
        public int StartBlocksCount { get; private set; }
        public int CurrentBlocksCount { get; private set; }
        
        private readonly HashSet<BlockBehaviour> _destroyedBlocks = new HashSet<BlockBehaviour>();
        
        public void Reset()
        {
            StartBlocksCount = 0;
            CurrentBlocksCount = 0;
            _destroyedBlocks.Clear();
        }

        public void CreateBlock(BlockDescriptor descriptor, Vector2Int position, Transform parent)
        {
            var block = Object.Instantiate(_blockPrefab, parent);
            block.transform.localPosition = new Vector3(
                position.x * _mapSettings.BlockSize.x - _mapSettings.MapSize.x / 2 + _mapSettings.BlockSize.x / 2,
                position.y * _mapSettings.BlockSize.y - _mapSettings.MapSize.y / 2 + _mapSettings.BlockSize.y / 2,
                0f
            );
            block.SpriteRenderer.sprite = descriptor.Sprite;
            block.transform.localScale = 0.2f * Vector3.one;
            block.gameObject.AddComponent<PolygonCollider2D>();

            StartBlocksCount++;
            CurrentBlocksCount++;
        }
        
        public void DestroyBlock(BlockBehaviour blockBehaviour)
        {
            if (!_destroyedBlocks.Add(blockBehaviour))
                return;
            
            var position = blockBehaviour.transform.position;
            position.z -= 3f;
            
            Object.Destroy(blockBehaviour.gameObject);
            CurrentBlocksCount--;
            
            if (Random.value < BoosterChance && _ballService.GetBallsCount() < MaxBallsCount)
            {
                var boosterDescriptor = _boostersCollection.Boosters[Random.Range(0, _boostersCollection.Boosters.Count)];
                var booster = _diContainer.InstantiatePrefabForComponent<BoosterBehaviour>(_boosterPrefab);
                booster.transform.position = position;
                booster.Setup(boosterDescriptor);
            }
        }
    }
}