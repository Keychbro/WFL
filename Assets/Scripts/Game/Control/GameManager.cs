using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Game;
using System.Linq;
using Kamen.DataSave;
using WOFL.UI;

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

        #endregion

        #region Control Methods

        public void StartBattle()
        {
            Fraction playerFraction = FractionManager.Instance.CurrentFraction;
            _alliedCastle.Initialize(playerFraction.CastleSettings, playerFraction.Units);
            _manaView.Initialize(_alliedCastle);
            _gameCardsPanel.Initialize(_alliedCastle, playerFraction.Units);

            if (DataSaveManager.Instance.MyData.UnitsDatas[0].CurrentLevel != 1)
            {
                DataSaveManager.Instance.MyData.UnitsDatas[0].IncreaseLevel();
                DataSaveManager.Instance.SaveData();
            }
        }

        #endregion
    }
}