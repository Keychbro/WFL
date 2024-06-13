using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Game;
using System.Linq;
using Kamen.DataSave;
using WOFL.UI;
using System.Threading.Tasks;
using Kamen.UI;
using WOFL.Settings;
using System;

namespace WOFL.Control
{
    public class GameManager : SingletonComponent<GameManager>
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Castle _alliedCastle;
        [SerializeField] private AIEnemy _aiEnemy;
        [SerializeField] private Castle _enemyCastle;
        [Space]
        [SerializeField] private ManaView _manaView;
        [SerializeField] private GameCardsPanel _gameCardsPanel;
        [SerializeField] private UpgradeScreen _upgradeScreen;
        [SerializeField] private FightScreen _fightScreen;
        [SerializeField] private EndGameScreenPopup _winPopup;
        [SerializeField] private EndGameScreenPopup _losePopup;

        [Header("Settings")]
        [SerializeField] private float _delayBetweenUpdateBattleControl;

        [Header("Variables")]
        private Coroutine _aiEnemyPlayCoroutine;
        private Coroutine _battleControlCoroutine;
        private int _levelNumber;

        #endregion

        #region Properties

        public bool IsBattleStarted { get; private set; }
        public event Action OnBattleStarted;
        public event Action OnBattleFinished;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _alliedCastle.OnDead -= CallLose;
            _enemyCastle.OnDead -= CallWin;
        }

        #endregion

        #region Control Methods

        public void CallUpdateLevel(AIEnemySettings enemySettings, int levelNumber)
        {
            _aiEnemy.UpdateLevelSettings(enemySettings);

            Fraction enemyFraction = _aiEnemy.EnemyFraction == Fraction.FractionName.Zombi ? FractionManager.Instance.ZombiFraction : FractionManager.Instance.GetFractionByName(_aiEnemy.EnemyFraction);
            _enemyCastle.CallUpdateCastleView(enemyFraction.CastleSettings);
            _levelNumber = levelNumber;
        }
        public void StartBattle()
        {
            Fraction playerFraction = FractionManager.Instance.CurrentFraction;
            UpgradeCastleCardLevelsHolder castleCardHealth = _upgradeScreen.UpgradeCastleCardLevelsHolders.First(card => card.Type == UpgradeCastleCardLevelsHolder.UpgradeCastleCardType.CastleHealth);
            int castleCardHealthLevel = DataSaveManager.Instance.MyData.GetUpgradeCastleCardDataByType(castleCardHealth.Type).Level;
            castleCardHealthLevel = castleCardHealthLevel < castleCardHealth.CardLeveles.Length ? castleCardHealthLevel : castleCardHealth.CardLeveles.Length - 1;

            UpgradeCastleCardLevelsHolder castleCardManaFill = _upgradeScreen.UpgradeCastleCardLevelsHolders.First(card => card.Type == UpgradeCastleCardLevelsHolder.UpgradeCastleCardType.ManaRegeneration);
            int castleCardManaFillLevel = DataSaveManager.Instance.MyData.GetUpgradeCastleCardDataByType(castleCardManaFill.Type).Level;
            castleCardManaFillLevel = castleCardManaFillLevel < castleCardManaFill.CardLeveles.Length ? castleCardManaFillLevel : castleCardManaFill.CardLeveles.Length - 1;

            _alliedCastle.Initialize( 
                playerFraction.Units,
                Mathf.RoundToInt(castleCardHealth.CardLeveles[castleCardHealthLevel].Value),
                castleCardManaFill.CardLeveles[castleCardManaFillLevel].Value,
                _aiEnemy.EnemyFraction);
            _alliedCastle.OnDead += CallLose;
            _manaView.Initialize(_alliedCastle);
            _gameCardsPanel.Initialize(_alliedCastle, playerFraction.Units);

            Fraction enemyFraction = _aiEnemy.EnemyFraction == Fraction.FractionName.Zombi ? FractionManager.Instance.ZombiFraction : FractionManager.Instance.GetFractionByName(_aiEnemy.EnemyFraction);

            _aiEnemyPlayCoroutine = StartCoroutine(_aiEnemy.Play());
            _enemyCastle.Initialize(
                enemyFraction.Units, 
                _aiEnemy.LevelSettings.CastleHealth,
                0,
                _aiEnemy.EnemyFraction);

            _enemyCastle.OnDead += CallWin;

            if (DataSaveManager.Instance.MyData.UnitsDatas[0].CurrentLevel != 1)
            {
                DataSaveManager.Instance.MyData.UnitsDatas[0].IncreaseLevel();
                DataSaveManager.Instance.SaveData();
            }

            IsBattleStarted = true;
            OnBattleStarted?.Invoke();
            _battleControlCoroutine = StartCoroutine(BattleControl());
        }
        public void CallTakeDamageInPointWithRadius(IDamageable.GameSideName targetSideName,Vector3 damagePosition, float radius, int damage)
        {
            switch (targetSideName)
            {
                case IDamageable.GameSideName.Allied:
                    _alliedCastle.TakeDamageInPointWithRadius(damagePosition, radius, damage);
                    break;
                case IDamageable.GameSideName.Enemy:
                    _enemyCastle.TakeDamageInPointWithRadius(damagePosition, radius, damage);
                    break;
            }
        }
        private IEnumerator BattleControl()
        {
            while (IsBattleStarted)
            {
                yield return new WaitForSeconds(_delayBetweenUpdateBattleControl);
                _alliedCastle.UpdateTargets(_enemyCastle);
                _enemyCastle.UpdateTargets(_alliedCastle);
            }
        }
        private void CallWin(IDeathable deathableObject)
        {
            if (!IsBattleStarted) return;

            IsBattleStarted = false;
            PopupManager.Instance.Show("WinScreenPopup");
            _winPopup.AdjustRewards(EndGameRewardManager.Instance.GetRewardList(EndGameRewardManager.EndGameType.Win));
            if (DataSaveManager.Instance.MyData.GameLevel < _levelNumber + 1 ) 
            {
                DataSaveManager.Instance.MyData.GameLevel = _levelNumber + 1;
            }
            DataSaveManager.Instance.SaveData();
        }
        private void CallLose(IDeathable deathableObject)
        {
            if (!IsBattleStarted) return;

            IsBattleStarted = false;
            PopupManager.Instance.Show("LoseScreenPopup");
            _losePopup.AdjustRewards(EndGameRewardManager.Instance.GetRewardList(EndGameRewardManager.EndGameType.Lose));
        }
        public void FinishBattle()
        {
            PopupManager.Instance.HideAllPopups();
            _fightScreen.FinishGame();
            OnBattleFinished?.Invoke();

            _alliedCastle.Clear();
            _enemyCastle.Clear();

            StopCoroutine(_aiEnemyPlayCoroutine);
            StopCoroutine(_battleControlCoroutine);
        }

        #endregion
    }
}