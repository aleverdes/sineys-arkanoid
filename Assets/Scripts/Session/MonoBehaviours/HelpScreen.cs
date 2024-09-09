using System;
using TaigaGames.SineysArkanoid.Session.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TaigaGames.SineysArkanoid.Session.MonoBehaviours
{
    public class HelpScreen : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        
        [Inject] private readonly SessionUIService _sessionUIService;
        
        private void Awake()
        {
            Time.timeScale = 0f;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnContinueButtonClicked();
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }
        
        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }
        
        private void OnContinueButtonClicked()
        {
            _sessionUIService.HideHelpScreen();
        }
    }
}