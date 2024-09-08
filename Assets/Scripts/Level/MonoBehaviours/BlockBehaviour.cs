using UnityEngine;

namespace TaigaGames.SineysArkanoid.Level.MonoBehaviours
{
    public class BlockBehaviour : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    }
}