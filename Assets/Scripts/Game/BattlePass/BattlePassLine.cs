using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Control;
using WOFL.Settings;
using WOFL.UI;

namespace WOFL.BattlePass
{
    public class BattlePassLine : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private BattlePassLevelView _battlePassLevelViewPrefab;

        [Header("Objects")]
        [SerializeField] private Slider _scoreLine;
        [SerializeField] private GameObject _levelHolder;

        [Header("Variables")]
        private List<BattlePassLevelView> _battlePassLevels = new List<BattlePassLevelView>();
        private BattlePassLineData _battlePassLineData;
        private BattlePassDataSave _battlePassDataSave;

        public event Action OnLineFilled;

        #endregion

        #region Control Methods

        public void Initialize(BattlePassLineData battlePassLineData, BattlePassDataSave battlePassDataSave)
        {
            _battlePassLineData = battlePassLineData;
            _battlePassDataSave = battlePassDataSave;

            int currentLevel = battlePassDataSave.Score / battlePassLineData.ScoreForOneLevel;

            _scoreLine.maxValue = (battlePassDataSave.TotalLevels + 1) * battlePassLineData.ScoreForOneLevel;
            _battlePassDataSave.OnScoreChanged += UpdateScoreView;
            UpdateScoreView();

            for (int i = 0; i < battlePassDataSave.TotalLevels; i++)
            {
                BattlePassLevelView newBattlePassLevel = Instantiate(_battlePassLevelViewPrefab, _levelHolder.transform);
                newBattlePassLevel.Initialize(i + 1, i + 1 <= currentLevel);

                newBattlePassLevel.AdjustRewardView(true, battlePassLineData.ClassicRewardLineInfo.RewardInfos[i], battlePassLineData.ClassicRewardLineInfo.RewardViewSettings, battlePassDataSave.ClassicRewardStates[i]);
                newBattlePassLevel.AdjustRewardView(false, battlePassLineData.ForPaidRewardLineInfo.RewardInfos[i], battlePassLineData.ForPaidRewardLineInfo.RewardViewSettings, battlePassDataSave.ForPaidRewardStates[i]);

                _battlePassLevels.Add(newBattlePassLevel);
            }
        }
        public void UpdateScoreView()
        {
            _scoreLine.value = _battlePassDataSave.Score;
            if (_scoreLine.value >= _scoreLine.maxValue - _battlePassLineData.ScoreForOneLevel)
            {
                _scoreLine.value = _scoreLine.maxValue;
                OnLineFilled?.Invoke();
            }
        }

        #endregion
    }
}