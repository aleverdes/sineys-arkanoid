using UnityEngine;

namespace TaigaGames.SineysArkanoid.Session.Services
{
    public class ProgressService
    {
        public int CurrentLevelIndex { get; private set; }
        public bool HelpShown { get; set; }
        
        public void Load()
        {
            CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 0);
            HelpShown = PlayerPrefs.GetInt("HelpShown", 0) == 1;
        }
        
        public void Save()
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex);
            PlayerPrefs.SetInt("HelpShown", HelpShown ? 1 : 0);
        }

        public void Open(int next)
        {
            CurrentLevelIndex = Mathf.Max(CurrentLevelIndex, next);
            Save();
        }
    }
}