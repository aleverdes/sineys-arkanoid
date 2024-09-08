using UnityEngine;

namespace TaigaGames.SineysArkanoid.Pad.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PadSettings", menuName = "SineysArkanoid/PadSettings")]
    public class PadSettings : ScriptableObject
    {
        [field: Header("Speeds")]
        [field: SerializeField] public float KeyboardMovementSpeed { get; private set; } = 10f;
        
        [field: Header("Sizes")]
        [field: SerializeField] public float BaseScale { get; private set; } = 0.25f;
        
        [field: Header("Launch")]
        [field: SerializeField] public float MaxLaunchAngle { get; private set; } = 45f;
    }
}