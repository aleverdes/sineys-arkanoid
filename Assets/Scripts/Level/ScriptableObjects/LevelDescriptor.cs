using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BlockDescriptor", menuName = "SineysArkanoid/Level")]
    public class LevelDescriptor : ScriptableObject
    {
        [field: SerializeField] public Vector2 Size { get; private set; } = Vector2.one;
        [field: SerializeField] public Texture2D LevelTexture { get; private set; }
    }
}