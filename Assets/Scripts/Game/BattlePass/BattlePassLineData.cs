using UnityEngine;
using WOFL.BattlePass;
using WOFL.UI;
using System;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Battle Pass Line Data", menuName = "WOFL/BattlePass/Pass/Battle Pass Line Data", order = 1)]
    public class BattlePassLineData : ScriptableObject
    {
        #region Classes

        [Serializable] public class RewardLineInfo
        {
            #region RewardLineInfo Variables

            [Header("Settings")]
            [SerializeField] private BattlePassRewardViewSettings _rewardViewSettings;
            [SerializeField] private BattlePassRewardInfo[] _rewardInfos;

            #endregion

            #region RewardLineInfo Properties

            public BattlePassRewardViewSettings RewardViewSettings { get => _rewardViewSettings; }
            public BattlePassRewardInfo[] RewardInfos { get => _rewardInfos; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private string _seasonName;
        [SerializeField] private RewardLineInfo _classicRewardLineInfo;
        [SerializeField] private RewardLineInfo _forPaidRewardLineInfo;
        [SerializeField] private int _scoreForOneLevel;

        #endregion

        #region Properties

        public string SeasonName { get => _seasonName; }
        public RewardLineInfo ClassicRewardLineInfo { get => _classicRewardLineInfo; }
        public RewardLineInfo ForPaidRewardLineInfo { get => _forPaidRewardLineInfo; }
        public int ScoreForOneLevel { get => _scoreForOneLevel; }

        #endregion
    }
}