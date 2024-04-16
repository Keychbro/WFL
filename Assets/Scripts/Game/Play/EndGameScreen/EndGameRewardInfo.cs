using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.Game
{
    [Serializable] public class EndGameRewardInfo
    {
        #region Varaibles

        [Header("Settings")]
        [SerializeField] private int _amount;
        [SerializeField] private EndGameRewardSettings _rewardSettings;
        [SerializeField] private Action<int> _callback;

        #endregion

        #region Properties

        public int Amount { get => _amount; }
        public EndGameRewardSettings RewardSettings { get => _rewardSettings; }
        public Action<int> Callback { get => _callback; } 

        #endregion

        #region Constructors

        public EndGameRewardInfo(int amount, EndGameRewardSettings rewardSettings, Action<int> callback)
        {
            _amount = amount;
            _rewardSettings = rewardSettings;
            _callback = callback;
        }

        #endregion

        #region Control Methods

        public void IncreaseByFactorValue(float factorValue) => _amount = Mathf.RoundToInt(_amount * factorValue);

        #endregion
    }
}