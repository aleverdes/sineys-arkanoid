using System;
using UnityEngine;

namespace TaigaGames.SineysArkanoid.Pad.MonoBehaviours
{
    public class PadArea : MonoBehaviour
    {
        [field: SerializeField] public float Width { get; private set; } = 6f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(Width, 0.1f, 0.1f));
        }
    }
}