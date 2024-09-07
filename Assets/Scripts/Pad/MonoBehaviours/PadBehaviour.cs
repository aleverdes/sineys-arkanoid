using UnityEngine;

namespace TaigaGames.SineysArkanoid.Pad.MonoBehaviours
{
    public class PadBehaviour : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public BoxCollider2D Collider { get; private set; }
    }
}