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
        [SerializeField] private Castle _enemyCastle;
        [Space]
        [SerializeField] private ManaView _manaView;
        [SerializeField] private GameCardsPanel _gameCardsPanel;

        [Header("Settings")]
        [SerializeField] private LevelSettings[] _levelSettings;
        [SerializeField] private float _delayBetweenUpdateBattleControl;

        #endregion

        #region Properties

        public bool IsBattleStarted { get; private set; }

        #endregion

        #region Control Methods

        public void StartBattle()
        {
            Fraction playerFraction = FractionManager.Instance.CurrentFraction;
            _alliedCastle.Initialize(playerFraction.CastleSettings, playerFraction.Units);
            _manaView.Initialize(_alliedCastle);
            _gameCardsPanel.Initialize(_alliedCastle, playerFraction.Units);

            _enemyCastle.Initialize(playerFraction.CastleSettings, playerFraction.Units);

            if (DataSaveManager.Instance.MyData.UnitsDatas[0].CurrentLevel != 1)
            {
                DataSaveManager.Instance.MyData.UnitsDatas[0].IncreaseLevel();
                DataSaveManager.Instance.SaveData();
            }

            IsBattleStarted = true;
            BattleControl();
        }
        private async void BattleControl()
        {
            while (IsBattleStarted)
            {
                await Task.Delay(Mathf.RoundToInt(_delayBetweenUpdateBattleControl * 1000));
                _alliedCastle.UpdateTargets(_enemyCastle);
                _enemyCastle.UpdateTargets(_alliedCastle);
            }
        }


        #endregion
    }
}