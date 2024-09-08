using UnityEngine;

namespace TaigaGames.SineysArkanoid.Session.MonoBehaviours
{
    public class SessionPauseScreen : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 0f;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}