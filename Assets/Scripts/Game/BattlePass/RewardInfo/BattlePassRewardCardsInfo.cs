using UnityEngine;
using UnityEngine.UI;
using System;
using WOFL.Game;

namespace WOFL.UI
{
    [CreateAssetMenu(fileName = "Battle Pass Reward Cards info", menuName = "WOFL/BattlePass/Battle Pass Reward Cards info", order = 1)]
    public abstract class BattlePassRewardCardsInfo : BattlePassRewardInfo
    {
        #region Classes

        [Serializable] public class RewardCardInfo
        {
            #region RewardCardInfo Variables

            [SerializeField] private Fraction.FractionName _fractionName;
            [SerializeField] private string _uniqueName;

            #endregion

            #region RewardCardInfo Properties

            public Fraction.FractionName FractionName { get => _fractionName; }
            public string UniqueName { get => _uniqueName; }

            #endregion
        }

        #endregion

        #region Variables
        
        [Header("Cards Settings")]
        [SerializeField] private RewardCardInfo[] _rewardCardInfos;
        
        #endregion
        
        #region Properties
        
        public RewardCardInfo[] RewardCardInfos { get => _rewardCardInfos; }
        
        #endregion
    }
}