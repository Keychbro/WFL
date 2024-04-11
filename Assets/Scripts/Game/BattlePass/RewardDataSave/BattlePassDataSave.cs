using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;
using WOFL.UI;

namespace WOFL.BattlePass
{
    [Serializable] public class BattlePassDataSave
    {
        #region Variables

        [SerializeField] private string _seasonName;
        [SerializeField] private List<RewardStateView.RewardState> _classicRewardStates;
        [SerializeField] private List<RewardStateView.RewardState> _forPaidRewardStates;
        [SerializeField] private readonly int _totalLevels;

        [SerializeField] private readonly int _maxScore;
        [SerializeField] private int _score;
        public event Action OnScoreChanged;

        #endregion

        #region Properties

        public string SeasonName { get => _seasonName; }
        public List<RewardStateView.RewardState> ClassicRewardStatas { get => _classicRewardStates; }
        public List<RewardStateView.RewardState> ForPaidRewardStates { get => _forPaidRewardStates; }
        public int TotalLevels { get => _totalLevels; }
        public int MaxScore { get => _maxScore; }
        public int Score { get => _score; }

        #endregion

        #region Constructors

        public BattlePassDataSave(BattlePassLineData battlePassLineData)
        {
            _seasonName = battlePassLineData.SeasonName;

            _totalLevels = GetTotalLevels(battlePassLineData.ClassicRewardInfos, battlePassLineData.ForPaidRewardInfos);
            _maxScore = _totalLevels * battlePassLineData.ScoreForOneLevel;
            _score = 0;

            AdjustRewardStates(battlePassLineData.ClassicRewardInfos, out _classicRewardStates);
            AdjustRewardStates(battlePassLineData.ForPaidRewardInfos, out _forPaidRewardStates);
        }

        #endregion

        #region Control Methods

        public void IncreaseScore(int value)
        {
            if (value < 0)
            {
                Debug.LogError("Trying to increase score on a negative value");
                return;
            }

            _score += value;
            OnScoreChanged?.Invoke();
        }

        #endregion

        #region Help Methods

        private int GetTotalLevels(BattlePassRewardInfo[] classicRewardInfos, BattlePassRewardInfo[] forPaidRewardInfos)
        {
            if (classicRewardInfos.Length >= forPaidRewardInfos.Length) return classicRewardInfos.Length;
            else return forPaidRewardInfos.Length;
        }
        private void AdjustRewardStates(BattlePassRewardInfo[] rewardInfos, out List<RewardStateView.RewardState> rewardStatas)
        {
            rewardStatas = new List<RewardStateView.RewardState>();

            for (int i = 0; i < rewardInfos.Length; i++)
            {
                rewardStatas.Add(RewardStateView.RewardState.Closed);
            }
        }

        #endregion
    }
}