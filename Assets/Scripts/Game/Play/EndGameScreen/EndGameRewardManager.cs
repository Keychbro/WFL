using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Settings;
using System.Linq;

namespace WOFL.Control
{
    public class EndGameRewardManager : SingletonComponent<EndGameRewardManager>
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private EndGameRewardSettings[] _rewardsSettings;

        #endregion

        #region Control Methods

        public EndGameRewardSettings GetEndGameRewardSettingsByType(EndGameRewardSettings.EndGameRewardType type)
        {
            return _rewardsSettings.First(rewardSettings => rewardSettings.RewardType == type);
        }

        #endregion
    }
}

