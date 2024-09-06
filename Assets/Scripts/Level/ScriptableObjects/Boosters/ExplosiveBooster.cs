using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters
{
    [CreateAssetMenu(fileName = "BlockDescriptor", menuName = "SineysArkanoid/Boosters/Expolsive")]
    public class ExplosiveBooster : BoosterDescriptor
    {
        [field: SerializeField] public GameObject ExplosionPrefab { get; private set; }
        [field: SerializeField] public float ExplosionRadius { get; private set; } = 0.16f;
        [field: SerializeField] public float ExplosionLifetime { get; private set; } = 3f;
    }
}