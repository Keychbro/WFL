using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Game;
using System.Linq;
using Kamen.DataSave;
using WOFL.UI;
using System.Threading.Tasks;

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

        [Header("Settings")]
        [SerializeField] private float _delayBetweenUpdateBattleControl;

        [Header("Variables")]
        private Coroutine _aiEnemyPlayCoroutine;
        private Coroutine _battleControlCoroutine;

        #endregion

        #region Properties

        public bool IsBattleStarted { get; private set; }

        #endregion

        #region Control Methods

        public void CallUpdateLevel(AIEnemySettings enemySettings)
        {
            _aiEnemy.UpdateLevelSettings(enemySettings);
        }
        public void StartBattle()
        {
            Fraction playerFraction = FractionManager.Instance.CurrentFraction;
            _alliedCastle.Initialize(playerFraction.CastleSettings, playerFraction.Units);
            _manaView.Initialize(_alliedCastle);
            _gameCardsPanel.Initialize(_alliedCastle, playerFraction.Units);

            _aiEnemyPlayCoroutine = StartCoroutine(_aiEnemy.Play());
            _enemyCastle.Initialize(playerFraction.CastleSettings, playerFraction.Units);

            if (DataSaveManager.Instance.MyData.UnitsDatas[0].CurrentLevel != 1)
            {
                DataSaveManager.Instance.MyData.UnitsDatas[0].IncreaseLevel();
                DataSaveManager.Instance.SaveData();
            }

            IsBattleStarted = true;
            _battleControlCoroutine = StartCoroutine(BattleControl());
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


        #endregion
    }
}