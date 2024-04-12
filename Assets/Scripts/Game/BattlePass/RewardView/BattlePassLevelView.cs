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

        public void Initialize(
            int levelNumber, 
            bool isOn, 
            BattlePassRewardInfo rewardInfo, 
            BattlePassRewardViewSettings battlePassRewardViewSettings,
            BattlePassDataSave.RewardStateData rewardStateData, 
            BattlePassRewardInfo forPaidRewardInfo, 
            BattlePassRewardViewSettings forPaidBattlePassRewardViewSettings,
            BattlePassDataSave.RewardStateData forPaidRewardStateData)
        {
            _levelPoint.Initialize(levelNumber, isOn);

            _classicRewardView.Initialize(rewardInfo, battlePassRewardViewSettings, rewardStateData);
            _forPaidRewardView.Initialize(forPaidRewardInfo, forPaidBattlePassRewardViewSettings, forPaidRewardStateData);
        }

        #endregion
    }
}