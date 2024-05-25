using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Settings;
using System.Linq;
using WOFL.Game;
using System;
using WOFL.UI;
using Random = UnityEngine.Random;
using Kamen.DataSave;

namespace WOFL.Control
{
    public class EndGameRewardManager : SingletonComponent<EndGameRewardManager>
    {
        #region Enums

        public enum EndGameType
        {
            Win,
            Lose
        }

        #endregion

        #region Classes

        [Serializable] private class RewardCreateInfo
        {
            #region RewardCreateInfo Variables

            [SerializeField] private EndGameRewardSettings.EndGameRewardType _type;
            [SerializeField][Range(0f, 100f)] private int _chance;
            [SerializeField] private int _minAmount;
            [SerializeField] private int _maxAmount;

            #endregion

            #region RewardCreateInfo Properties

            public EndGameRewardSettings.EndGameRewardType Type { get => _type; }
            public float Chance { get => _chance; }
            public int MinAmount { get => _minAmount; }
            public int MaxAmount { get => _maxAmount; }

            #endregion
        }
        [Serializable] private class EndGameResultInfo
        {
            #region EndGameTypeInfo Variables

            [SerializeField] private EndGameType _endGameType;
            [SerializeField] private RewardCreateInfo[] _rewardCreateInfos;

            #endregion

            #region EndGameTypeInfo Properties

            public EndGameType EndGameType { get => _endGameType; }
            public RewardCreateInfo[] RewardCreateInfos { get => _rewardCreateInfos; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private UpgradeScreen _upgradeScreen;

        [Header("Settings")]
        [SerializeField] private EndGameRewardSettings[] _rewardsSettings;
        [SerializeField] private EndGameResultInfo[] _endGameResultInfos;

        #endregion

        #region Control Methods

        public EndGameRewardSettings GetEndGameRewardSettingsByType(EndGameRewardSettings.EndGameRewardType type)
        {
            return _rewardsSettings.First(rewardSettings => rewardSettings.RewardType == type);
        }
        public List<EndGameRewardInfo> GetRewardList(EndGameType endGameType)
        {
            List<EndGameRewardInfo> newRewardsInfos = new List<EndGameRewardInfo>();
            EndGameResultInfo resultInfo = _endGameResultInfos.First(endGameResultInfo => endGameResultInfo.EndGameType == endGameType);

            for (int i = 0; i < _rewardsSettings.Length; i++)
            {
                RewardCreateInfo currentRewardInfo = resultInfo.RewardCreateInfos.First(info => info.Type == _rewardsSettings[i].RewardType);
                int rollValue = Random.Range(1, 101);
                if (rollValue <= currentRewardInfo.Chance)
                {
                    EndGameRewardInfo newReward = new EndGameRewardInfo(
                        Random.Range(currentRewardInfo.MinAmount, currentRewardInfo.MaxAmount) * (1 + ((DataSaveManager.Instance.MyData.GameLevel + 1) / 10)),
                        _rewardsSettings.First(rewardSettings => rewardSettings.RewardType == currentRewardInfo.Type));
                    
                    if (currentRewardInfo.Type != EndGameRewardSettings.EndGameRewardType.DiamondsWithPass)
                    {
                        newRewardsInfos.Add(newReward);
                    }
                    else if (DataSaveManager.Instance.MyData.DiamondPassDataSave.IsPassPurchased)
                    {
                        newRewardsInfos.Add(newReward);
                    }
                }
            }

            return newRewardsInfos;
        }

        #endregion
    }
}

