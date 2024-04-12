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
        #region Classes

        [Serializable] public class RewardStateData
        {
            #region RewardStateData Variables

            [SerializeField] private RewardStateView.RewardState _rewardState;
            public event Action OnRewardStateChanged;

            #endregion

            #region RewardStateData Properties

            public RewardStateView.RewardState RewardState { get => _rewardState; }

            #endregion

            #region Constructors

            public RewardStateData()
            {
                _rewardState = RewardStateView.RewardState.Closed;
            }

            #endregion

            #region RewardStateData Control Methods

            public void NextRewardState()
            {
                switch (_rewardState)
                {
                    case RewardStateView.RewardState.Closed:
                        _rewardState = RewardStateView.RewardState.Ready;
                        break;
                    case RewardStateView.RewardState.Ready:
                        _rewardState = RewardStateView.RewardState.Accepted;
                        break;
                    case RewardStateView.RewardState.Accepted:
                        _rewardState = RewardStateView.RewardState.Accepted;
                        break;
                }

                OnRewardStateChanged?.Invoke();
            }


            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private string _seasonName;
        [SerializeField] private List<RewardStateData> _classicRewardStates;
        [SerializeField] private List<RewardStateData> _forPaidRewardStates;
        [SerializeField] private int _totalLevels;
        [SerializeField] private int _maxScore;
        [SerializeField] private int _score;
        [SerializeField] private bool _isPassPurchased;
        public event Action OnScoreChanged;

        [Header("Additional Variables")]
        [SerializeField] private int _scoreForOneLevel;

        #endregion

        #region Properties

        public string SeasonName { get => _seasonName; }
        public List<RewardStateData> ClassicRewardStates { get => _classicRewardStates; }
        public List<RewardStateData> ForPaidRewardStates { get => _forPaidRewardStates; }
        public int TotalLevels { get => _totalLevels; }
        public int MaxScore { get => _maxScore; }
        public int Score { get => _score; }
        public bool IsPassPurchased { get => _isPassPurchased; }

        #endregion

        #region Constructors

        public BattlePassDataSave(BattlePassLineData battlePassLineData)
        {
            _seasonName = battlePassLineData.SeasonName;

            _totalLevels = GetTotalLevels(battlePassLineData.ClassicRewardLineInfo.RewardInfos, battlePassLineData.ForPaidRewardLineInfo.RewardInfos);
            _maxScore = _totalLevels * battlePassLineData.ScoreForOneLevel;
            _score = 0;
            _scoreForOneLevel = battlePassLineData.ScoreForOneLevel;

            AdjustRewardStates(battlePassLineData.ClassicRewardLineInfo.RewardInfos, out _classicRewardStates);
            AdjustRewardStates(battlePassLineData.ForPaidRewardLineInfo.RewardInfos, out _forPaidRewardStates);
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
            UpdateAllRewardStates();
        }
        public void UpdateAllRewardStates()
        {
            int currentLevel = _score / _scoreForOneLevel;
            UpdateRewardList(currentLevel, _classicRewardStates);
            UpdateRewardList(currentLevel, _forPaidRewardStates);
        }
        private void UpdateRewardList(int currentLevel, List<RewardStateData> rewardStates)
        {
            for (int i = 0; i < rewardStates.Count; i++)
            {
                if (currentLevel < i + 1 || rewardStates[i].RewardState != RewardStateView.RewardState.Closed) continue;

                rewardStates[i].NextRewardState();
            }
        }
        public void PurchasePass()
        {
            _isPassPurchased = true;
        }

        #endregion

        #region Help Methods

        private int GetTotalLevels(BattlePassRewardInfo[] classicRewardInfos, BattlePassRewardInfo[] forPaidRewardInfos)
        {
            if (classicRewardInfos.Length >= forPaidRewardInfos.Length) return classicRewardInfos.Length;
            else return forPaidRewardInfos.Length;
        }
        private void AdjustRewardStates(BattlePassRewardInfo[] rewardInfos, out List<RewardStateData> rewardStatas)
        {
            rewardStatas = new List<RewardStateData>();

            for (int i = 0; i < rewardInfos.Length; i++)
            {
                rewardStatas.Add(new RewardStateData());
            }
        }

        #endregion
    }
}