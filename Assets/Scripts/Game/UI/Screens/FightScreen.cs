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
using System.Linq;

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
            [SerializeField] private KamenButton _panel;
            [SerializeField] private LevelSettings[] _levelSettings;
            private int _currentLevels;

            #endregion

            #region FightModeInfo Properties

            public FightModeType Type { get => _type; }
            public KamenButton Panel { get => _panel; }
            public LevelSettings[] LevelSettings { get => _levelSettings; }
            public int CurrentLevels 
            { 
                get => _currentLevels; 
                set
                {
                    if (value >= 0) _currentLevels = value;
                }
            }

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
        [SerializeField] private GameObject _modePanelsHolder;

        [Header("Settings")]
        [SerializeField] private FightModeInfo[] _fightModeInfo;

        [Header("Variables")]
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

            AdjustFightMode();

            _levelInfoView.Initialize();
            _levelInfoView.OnCallPreviousLevel += ShowPreviousLevel;
            _levelInfoView.OnCallNextLevel += ShowNextLevel;
            _levelInfoView.OnCallStartGame += StartGame;

            UdpateAllLevelView();
        }
        private void AdjustFightMode()
        {
            _currentFightModeInfo = _fightModeInfo.First(fightMode => fightMode.Type == FightModeType.Classic);
            for (int i = 0; i < _fightModeInfo.Length; i++)
            {
                _fightModeInfo[i].Panel.Initialize();
                _fightModeInfo[i].Panel.OnClick().AddListener(SwitchGameMode);
                _fightModeInfo[i].Panel.gameObject.SetActive(false);
            }

            _currentFightModeInfo.Panel.gameObject.SetActive(true);

            FightModeInfo classicMode = _fightModeInfo.First(fightMode => fightMode.Type == FightModeType.Classic);
            classicMode.CurrentLevels = DataSaveManager.Instance.MyData.GameLevel;
            if (classicMode.CurrentLevels > classicMode.LevelSettings.Length - 1) classicMode.CurrentLevels = classicMode.LevelSettings.Length - 1;

            FightModeInfo zombieMode = _fightModeInfo.First(fightMode => fightMode.Type == FightModeType.Zombie);
            zombieMode.CurrentLevels = DataSaveManager.Instance.MyData.GameLevel;
            if (zombieMode.CurrentLevels > zombieMode.LevelSettings.Length - 1) zombieMode.CurrentLevels = zombieMode.LevelSettings.Length - 1;
        }
        private void SwitchGameMode()
        {
            _currentFightModeInfo.Panel.gameObject.SetActive(false);
            _currentFightModeInfo = _fightModeInfo.First(fightMode => fightMode.Type != _currentFightModeInfo.Type);
            _currentFightModeInfo.Panel.gameObject.SetActive(true);
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
            _currentFightModeInfo.CurrentLevels--;
            UdpateAllLevelView();
        }
        private void ShowNextLevel()
        {
            _currentFightModeInfo.CurrentLevels++;
            if (_currentFightModeInfo.CurrentLevels >= _currentFightModeInfo.LevelSettings.Length) _currentFightModeInfo.CurrentLevels = _currentFightModeInfo.LevelSettings.Length;
            UdpateAllLevelView();
        }
        private void UdpateAllLevelView()
        {
            _levelInfoView.UpdateCurrentLevel(
                _currentFightModeInfo.Type == FightModeType.Zombie ? FractionManager.Instance.ZombiFraction :
                FractionManager.Instance.GetFractionByName(_currentFightModeInfo.LevelSettings[_currentFightModeInfo.CurrentLevels].AIEnemySettings.EnemyFractionName),
                _currentFightModeInfo.CurrentLevels,
                _currentFightModeInfo.LevelSettings.Length);
            GameManager.Instance.CallUpdateLevel(_currentFightModeInfo.LevelSettings[_currentFightModeInfo.CurrentLevels].AIEnemySettings, _currentFightModeInfo.CurrentLevels);
        }
        private void StartGame()
        {
            _mainTopBar.SwitchVisible(false, true);
            _quickBar.SwitchVisible(false, true);
            _miniProfileIcon.gameObject.SetActive(false);
            _levelInfoView.gameObject.SetActive(false);
            _modePanelsHolder.SetActive(false);

            _gameBottomBar.SetActive(true);

            GameManager.Instance.StartBattle();
        }
        public void FinishGame()
        {
            _mainTopBar.SwitchVisible(true, true);
            _quickBar.SwitchVisible(true, true);
            _miniProfileIcon.gameObject.SetActive(true);
            _levelInfoView.gameObject.SetActive(true);
            _modePanelsHolder.SetActive(true);

            _gameBottomBar.SetActive(false);
        }

        #endregion
    }
}