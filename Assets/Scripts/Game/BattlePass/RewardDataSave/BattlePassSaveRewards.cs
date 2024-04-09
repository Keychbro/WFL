using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.UI;

namespace WOFL.BattlePass
{
    [Serializable] public class BattlePassSaveRewards
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private List<RewardStateView.RewardState> _classicRewardStates;
        [SerializeField] private List<RewardStateView.RewardState> _forPaidRewardStates;

        #endregion

        #region Control Methods



        #endregion
    }
}