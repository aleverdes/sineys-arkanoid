using System;
using TaigaGames.SineysArkanoid.MainMenu.Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TaigaGames.SineysArkanoid.MainMenu.MonoBehaviours
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        
        public int LevelIndex { get; private set; }
        
        public event Action<LevelButton> OnClick;
        
        public void Setup(int levelIndex, LevelState levelState)
        {
            LevelIndex = levelIndex;
            _text.text = $"Level {levelIndex + 1}";

            switch (levelState)
            {
                case LevelState.Completed:
                    _button.image.color = Color.green;
                    break;
                case LevelState.Current:
                    _button.image.color = Color.white;
                    break;
                case LevelState.Locked:
                    _button.image.color = Color.grey;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(levelState), levelState, null);
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickHandler);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            OnClick?.Invoke(this);
        }
    }
}