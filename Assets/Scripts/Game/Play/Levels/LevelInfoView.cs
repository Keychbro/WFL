using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Control;
using System;
using Unity.VisualScripting;
using WOFL.Game;
using Kamen.DataSave;
using System.Linq;

namespace WOFL.UI
{
    public class LevelInfoView : MonoBehaviour
    {
        #region Enums

        private enum ResultLevel
        {
            Completed,
            Ready,
            Closed
        }

        #endregion

        #region Classes

        [Serializable] private class ResultLevelInfo
        {
            #region ResultLevelInfo Variables

            [SerializeField] private ResultLevel _resultLevel;
            [SerializeField] private Sprite _icon;

            #endregion

            #region ResultLevelInfo Properties

            public ResultLevel ResultLevel { get => _resultLevel; }
            public Sprite Icon { get => _icon; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private KamenButton _leftArrow;
        [SerializeField] private KamenButton _rightArrow;
        [SerializeField] private KamenButton _playButton;
        [SerializeField] private TextMeshProUGUI _levelText;
        [Space]
        [SerializeField] private Image _alliedIcon;
        [SerializeField] private Image _enemyIcon;
        [SerializeField] private Image _resultLevelIcon;

        [Header("Settings")]
        [SerializeField] private ResultLevelInfo[] _resultLevelInfo;

        [Header("Variables")]
        private ResultLevel _currentResult;

        public event Action OnCallPreviousLevel;
        public event Action OnCallNextLevel;
        public event Action OnCallStartGame;

        #endregion

        #region Control Methods

        public void Initialize()
        {
            _leftArrow.Initialize();
            _leftArrow.OnClick().AddListener(ShowPreviousLevel);

            _rightArrow.Initialize();
            _rightArrow.OnClick().AddListener(ShowNextLevel);

            _playButton.Initialize();
            _playButton.OnClick().AddListener(StartGame);
        }
        private void ShowPreviousLevel() => OnCallPreviousLevel?.Invoke();
        private void ShowNextLevel() => OnCallNextLevel?.Invoke();
        private void StartGame() => OnCallStartGame?.Invoke();

        public void UpdateCurrentLevel(Fraction enemyFraction, int levelNumber, int maxLevel)
        {
            _alliedIcon.sprite = FractionManager.Instance.CurrentFraction.Logo;
            _enemyIcon.sprite = enemyFraction.Logo;

            _levelText.text = $"LEVEL {levelNumber + 1}";
            (_resultLevelIcon.sprite, _currentResult) = GetResultLevelIcon(levelNumber);

            if (_currentResult == ResultLevel.Closed) _playButton.ChangeInteractable(false);
            else _playButton.ChangeInteractable(true);

            if (levelNumber <= 0) _leftArrow.ChangeInteractable(false);
            else _leftArrow.ChangeInteractable(true);

            if (levelNumber >= maxLevel - 1) _rightArrow.ChangeInteractable(false);
            else _rightArrow.ChangeInteractable(true);
        }
        private (Sprite, ResultLevel) GetResultLevelIcon(int levelNumber)
        {
            if (levelNumber < DataSaveManager.Instance.MyData.GameLevel) return (_resultLevelInfo.First(resultLevelInfo => resultLevelInfo.ResultLevel == ResultLevel.Completed).Icon, ResultLevel.Completed);
            else if (levelNumber == DataSaveManager.Instance.MyData.GameLevel) return (_resultLevelInfo.First(resultLevelInfo => resultLevelInfo.ResultLevel == ResultLevel.Ready).Icon, ResultLevel.Ready);
            else return (_resultLevelInfo.First(resultLevelInfo => resultLevelInfo.ResultLevel == ResultLevel.Closed).Icon, ResultLevel.Closed);
        }

        #endregion
    }
}