using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Settings;

namespace WOFL.BattlePass
{
    public class BattlePassLevelView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private BattlePassRewardView _classicRewardView;
        [SerializeField] private BattlePassRewardView _forPaidRewardView;
        [SerializeField] private BattlePassLevelPoint _levelPoint;

        //[Header("Variables")]


        #endregion

        #region Control Methods

        public void Initialize(int levelNumber, bool isOn)
        {
            _levelPoint.Initialize(levelNumber, isOn);
        }
        public void AdjustRewardView(bool isClassic, BattlePassRewardInfo rewardInfo, BattlePassRewardViewSettings battlePassRewardViewSettings, BattlePassDataSave.RewardStateData rewardStateData)
        {
            if (isClassic) _classicRewardView.Initialize(rewardInfo, battlePassRewardViewSettings, rewardStateData);
            else _forPaidRewardView.Initialize(rewardInfo, battlePassRewardViewSettings, rewardStateData);
        }

        #endregion
    }
}