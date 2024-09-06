using TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters;
using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BlockDescriptor", menuName = "SineysArkanoid/Block")]
    public class BlockDescriptor : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField, ColorUsage(false, false)] public Color EditorColor { get; private set; } = Color.white;
        [field: SerializeField] public BoosterDescriptor[] Boosters { get; private set; }
    }
}