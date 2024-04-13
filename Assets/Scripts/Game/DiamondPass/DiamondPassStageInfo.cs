using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WOFL.DiamondPass
{
    [CreateAssetMenu(fileName = "Diamond Pass Level Info", menuName = "WOFL/DiamondPass/Stages/Diamond Pass Level Info", order = 1)]
    public class DiamondPassStageInfo : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private int _targetLevel;
        [SerializeField] private int _amountDiamondsReward;

        #endregion

        #region Properties

        public int TargetLevel { get => _targetLevel; }
        public int AmountDiamondsReward { get => _amountDiamondsReward; }

        #endregion
    }
}