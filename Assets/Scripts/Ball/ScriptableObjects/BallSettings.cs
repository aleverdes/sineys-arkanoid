using UnityEngine;

namespace TaigaGames.SineysArkanoid.Ball.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BallSettings", menuName = "SineysArkanoid/BallSettings")]
    public class BallSettings : ScriptableObject
    {
        [field: SerializeField] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField] public float MaxSpeed { get; private set; } = 15f;
        [field: SerializeField] public float IncreaseSpeedPerSecond { get; private set; } = 0.085f;
    }
}