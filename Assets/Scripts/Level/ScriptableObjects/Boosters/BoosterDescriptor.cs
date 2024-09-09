using UnityEngine;
using Zenject;

namespace TaigaGames.SineysArkanoid.Level.ScriptableObjects.Boosters
{
    public abstract class BoosterDescriptor : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        
        public abstract void Execute(DiContainer container);
    }
}