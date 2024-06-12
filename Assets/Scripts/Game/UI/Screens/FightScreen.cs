using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using DG.Tweening;
using Kamen;
using Kamen.DataSave;
using WOFL.Game;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using WOFL.Control;
using System;

namespace WOFL.UI
{
    public class FightScreen : Kamen.UI.Screen
    {
        #region Enums

        public enum FightModeType
        {
            Classic,
            Zombie
        }

        #endregion

        #region Classes

        [Serializable] private class FightModeInfo
        {
            #region FightModeInfo Variables

            [SerializeField] private FightModeType _type;
            [SerializeField] private GameObject _panel;
            [SerializeField] private LevelSettings[] _levelSettings;

            #endregion

            #region FightModeInfo Properties

            public FightModeType Type { get => _type; }
            public GameObject Panel { get => _panel; }
            public LevelSettings[] LevelSettings { get => _levelSettings; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private LevelInfoView _levelInfoView;
        [Space]
        [SerializeField] private MainTopBar _mainTopBar;
        [SerializeField] private QuickBar _quickBar;
        [SerializeField] private MiniProfileIcon _miniProfileIcon;
        [SerializeField] private GameObject _gameBottomBar;

        [Header("Settings")]
        [SerializeField] private LevelSettings[] _levelSettings;
        [SerializeField] private FightModeInfo[] _fightModeInfo;

        [Header("Variables")]
        private int _currentLevel;
        private FightModeInfo _currentFightModeInfo;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _levelInfoView.OnCallPreviousLevel -= ShowPreviousLevel;
            _levelInfoView.OnCallNextLevel -= ShowNextLevel;
            _levelInfoView.OnCallStartGame -= StartGame;
        }
        #endregion

        #region Control Methods

        public override async void Initialize()
        {
            base.Initialize();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            await UniTask.WaitUntil(() => DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None);
            await Task.Delay(100);

            _currentLevel = DataSaveManager.Instance.MyData.GameLevel;

            _levelInfoView.Initialize();
            _levelInfoView.OnCallPreviousLevel += ShowPreviousLevel;
            _levelInfoView.OnCallNextLevel += ShowNextLevel;
            _levelInfoView.OnCallStartGame += StartGame;

            UdpateAllLevelView();
        }
        public override void Transit(bool isShow, bool isForth, ScreenManager.TransitionType type, float duration, Ease curve, MyCurve myCurve)
        {
            base.Transit(isShow, isForth, type, duration, curve, myCurve);

            if (DataSaveManager.Instance.MyData.ChoosenFraction == Fraction.FractionName.None)
            {
                PopupManager.Instance.Show("ChooseFractionPopup");
            }
        }
        private void ShowPreviousLevel()
        {
            _currentLevel--;
            UdpateAllLevelView();
        }
        private void ShowNextLevel()
        {
            _currentLevel++;
            UdpateAllLevelView();
        }
        private void UdpateAllLevelView()
        {
            _levelInfoView.UpdateCurrentLevel(
                FractionManager.Instance.GetFractionByName(_levelSettings[_currentLevel].AIEnemySettings.EnemyFractionName),
                _currentLevel,
                _levelSettings.Length);
            GameManager.Instance.CallUpdateLevel(_levelSettings[_currentLevel].AIEnemySettings, _currentLevel);
        }
        private void StartGame()
        {
            _mainTopBar.SwitchVisible(false, true);
            _quickBar.SwitchVisible(false, true);
            _miniProfileIcon.gameObject.SetActive(false);
            _levelInfoView.gameObject.SetActive(false);

            _gameBottomBar.SetActive(true);

            GameManager.Instance.StartBattle();
        }
        public void FinishGame()
        {
            _mainTopBar.SwitchVisible(true, true);
            _quickBar.SwitchVisible(true, true);
            _miniProfileIcon.gameObject.SetActive(true);
            _levelInfoView.gameObject.SetActive(true);

            _gameBottomBar.SetActive(false);
        }

        #endregion
    }
}