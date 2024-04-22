using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.DataSave
{
    [Serializable] public class UpgradeCastleCardData
    {
        #region Variables

        [SerializeField] private UpgradeCastleCardLevelsHolder.UpgradeCastleCardType _type;
        [SerializeField] private int _level;
        public event Action OnLevelChanged;

        #endregion

        #region Properties

        public UpgradeCastleCardLevelsHolder.UpgradeCastleCardType Type { get => _type; }
        public int Level { get => _level; }

        #endregion

        #region Constructors

        public UpgradeCastleCardData(UpgradeCastleCardLevelsHolder.UpgradeCastleCardType type)
        {
            _type = type;
            _level = 0;
        }

        #endregion

        #region Control Methods

        public void IncreaseLevel()
        {
            _level++;
            OnLevelChanged?.Invoke();
        }

        #endregion
    }
}