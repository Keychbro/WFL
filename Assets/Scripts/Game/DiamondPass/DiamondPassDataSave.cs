using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace WOFL.DiamondPass
{
    [Serializable] public class DiamondPassDataSave
    {
        #region Classes

        [Serializable] public class StageData
        {
            #region StageInfo Variables

            [SerializeField] private int _targetLevel;
            [SerializeField] private bool _isRewardReceived;

            public event Action OnRewardReceived;
            public event Action<int> OnExtraCallReward;

            #endregion

            #region StageInfo Properties

            public int TargetLevel { get => _targetLevel; }
            public bool IsRewardReceived { get => _isRewardReceived; }

            #endregion

            #region StageInfo Constructors

            public StageData(DiamondPassStageInfo levelInfo)
            {
                _targetLevel = levelInfo.TargetLevel;
                _isRewardReceived = false;
            }

            #endregion

            #region StageInfo Control Methods

            public void CallReceive()
            {
                _isRewardReceived = true;
                OnRewardReceived?.Invoke();
            }
            public void CallExtraCaLLReward() => OnExtraCallReward?.Invoke(_targetLevel);

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private bool _isPassPurchased;
        [SerializeField] private List<StageData> _stageInfos = new List<StageData>();

        public event Action OnPurchased;

        #endregion

        #region Properties

        public bool IsPassPurchased { get => _isPassPurchased; }
        public List<StageData> StageInfos { get => _stageInfos; }

        #endregion

        #region Control Methods

        public void AdjustDiamondPassStages(DiamondPassStageInfo[] levelInfos)
        {
            for (int i = 0; i < levelInfos.Length; i++)
            {
                if (!_stageInfos.Any(stageInfo => stageInfo.TargetLevel == levelInfos[i].TargetLevel))
                {
                    _stageInfos.Add(new StageData(levelInfos[i]));
                }
            }
            _stageInfos.OrderBy(stageInfo => stageInfo.TargetLevel);

            OnPurchased += CheckStagesRewards;
        }
        public StageData GetStageInfoByLevel(int targetLevel)
        {
            return _stageInfos.First(stageInfo => stageInfo.TargetLevel == targetLevel);
        }
        private void CheckStagesRewards()
        {
            for (int i = 0; i < _stageInfos.Count; i++)
            {
                if (DataSaveManager.Instance.MyData.GameLevel >= _stageInfos[i].TargetLevel && _isPassPurchased && !_stageInfos[i].IsRewardReceived)
                {
                    _stageInfos[i].CallExtraCaLLReward();
                    _stageInfos[i].CallReceive();
                }
            }
        }
        public void PurchasePass()
        {
            _isPassPurchased = true;
            OnPurchased?.Invoke();
        }

        #endregion
    }
}