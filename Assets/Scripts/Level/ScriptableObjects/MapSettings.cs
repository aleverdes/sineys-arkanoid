using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MapSettings", menuName = "SineysArkanoid/MapSettings")]
    public class MapSettings : ScriptableObject
    {
        [field: SerializeField] public Vector2 Center { get; private set; } = Vector2.zero;
        [field: SerializeField] public float ZIndex { get; private set; } = -5f;
        [field: SerializeField] public Vector2 MapSize { get; private set; } = Vector2.one;
        [field: SerializeField] public Vector2 BlockSize { get; private set; } = new Vector2(0.16f, 0.16f);
    }
}