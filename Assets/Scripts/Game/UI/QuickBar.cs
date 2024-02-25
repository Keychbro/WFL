using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Settings;
using System;
using System.Linq;
using Kamen.UI;

namespace WOFL.UI
{
    public class QuickBar : SingletonComponent<QuickBar>
    {
        #region Classes

        [Serializable] private struct ButtonWorkInfo
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

        [Header("Settings")]
        [SerializeField] private ButtonWorkInfo[] _buttonsList;

        [Header("Variables")]
        private ButtonWorkInfo _currentSelectedButton;

        #endregion

        #region Unity Methods

        private void Start()
        {
            for (int i = 0; i < _buttonsList.Length; i++)
            {
                _buttonsList[i].Button.CallChangeButtonState(QuickBarButtonInfo.ButtonState.Enabling, true);
                _buttonsList[i].Button.Initialize();
                _buttonsList[i].Button.OnClicked += SwitchButtons;
            }

            _currentSelectedButton = _buttonsList.First(buttonInfo => buttonInfo.LinkedScreenName == ScreenManager.Instance.StartScreen);
            _currentSelectedButton.Button.CallChangeButtonState(QuickBarButtonInfo.ButtonState.Selecting);
        }

        #endregion

        #region Control Methods

        private void SwitchButtons(QuickBarButton newSelectedButton)
        {
            if (newSelectedButton == _currentSelectedButton.Button) return;

            _currentSelectedButton.Button.CallChangeButtonState(QuickBarButtonInfo.ButtonState.Enabling);
            newSelectedButton.CallChangeButtonState(QuickBarButtonInfo.ButtonState.Selecting);

            _currentSelectedButton = _buttonsList.First(buttonInfo => buttonInfo.Button == newSelectedButton);
            ScreenManager.Instance.TransitionTo(_currentSelectedButton.LinkedScreenName);
        }

        #endregion
    }
}