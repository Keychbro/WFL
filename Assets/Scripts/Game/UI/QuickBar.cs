using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Settings;
using System;
using System.Linq;
using Kamen.UI;
using DG.Tweening;
using System.Threading.Tasks;

namespace WOFL.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class QuickBar : SingletonComponent<QuickBar>
    {
        #region Classes

        [Serializable] private class ButtonWorkInfo
        {
            #region ButtonWorkInfo Variables

            [SerializeField] private string _linkedScreenName;
            [SerializeField] private QuickBarButton _button;

            #endregion

            #region ButtonWorkInfo Properties

            public string LinkedScreenName { get => _linkedScreenName; }
            public QuickBarButton Button { get => _button; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private ScreenManager _screenManager;
        private CanvasGroup _canvasGroup;

        [Header("Settings")]
        [SerializeField] private Ease _switchVisibleEase;
        [SerializeField] private ButtonWorkInfo[] _buttonsList;

        [Header("Variables")]
        private ButtonWorkInfo _currentSelectedButton;
        private ButtonWorkInfo _oldSelectedButton;
        private bool _isVisible = true;
        private bool _isTransiting;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region Control Methods

        public void CallSwitchButtonByName(string name)
        {
            SwitchButtons(_buttonsList.First(button => button.LinkedScreenName == name).Button);
        }
        private async void SwitchButtons(QuickBarButton newSelectedButton)
        {
            if (newSelectedButton == _currentSelectedButton?.Button || _isTransiting) return;

            _isTransiting = true;
            _oldSelectedButton = _currentSelectedButton;
            _currentSelectedButton = _buttonsList.First(buttonInfo => buttonInfo.Button == newSelectedButton);

            ScreenManager.Instance.TransitionTo(_currentSelectedButton.LinkedScreenName);
            await Task.Delay(Mathf.RoundToInt(1000 * ScreenManager.Instance.TransitionDuration));
            _isTransiting = false;
        }
        private void Initialize()
        {
            _screenManager.OnScreenChanged += UpdateBarView;
            _canvasGroup = GetComponent<CanvasGroup>();

            for (int i = 0; i < _buttonsList.Length; i++)
            {
                _buttonsList[i].Button.CallChangeButtonState(QuickBarButtonInfo.ButtonState.Enabling, true);
                _buttonsList[i].Button.Initialize();
                _buttonsList[i].Button.OnClicked += SwitchButtons;
            }
        }
        private void UpdateBarView(ScreenManager.ScreenInfo screenInfo, bool isFast)
        {
            SwitchVisible(screenInfo.IsShowQuickButtons, isFast);

            if (_currentSelectedButton != null) _currentSelectedButton.Button.CallChangeButtonState(QuickBarButtonInfo.ButtonState.Selecting);
            if (_oldSelectedButton != null) _oldSelectedButton.Button.CallChangeButtonState(QuickBarButtonInfo.ButtonState.Enabling);
        }
        private async void SwitchVisible(bool isShow, bool isFast)
        {
            if (_isVisible == isShow) return;

            if (isShow)
            {
                gameObject.SetActive(true);
                _canvasGroup.alpha = 0;
            }

            _canvasGroup.DOFade(isShow ? 1 : 0, isFast ? 0 : _screenManager.TransitionDuration).SetEase(_switchVisibleEase);
            await Task.Delay(Mathf.RoundToInt(1000 * (isFast ? 0 : _screenManager.TransitionDuration)));

            if (!isShow) gameObject.SetActive(false);
            _isVisible = isShow;
        }

        #endregion
    }
}