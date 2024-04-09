using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.UI;
using WOFL.Settings;

namespace WOFL.BattlePass
{
    public class BattlePassRewardView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] protected Image _background;
        [SerializeField] protected Image _icon;
        [SerializeField] protected TextMeshProUGUI _amountText;
        [SerializeField] protected RewardStateView _rewardState;
        [Space]
        [SerializeField] protected Button _classicClaimButton;
        [SerializeField] protected Button _paidClaimButton;

        [Header("Variables")]
        protected BattlePassRewardViewSettings _battlePassRewardViewSettings;
        protected Button _currentUsedButton;

        #endregion

        #region Control Methods

        public virtual void Initialize(BattlePassRewardInfo rewardInfo, BattlePassRewardViewSettings battlePassRewardViewSettings,BattlePassLineData.RewardType type)
        {
            _battlePassRewardViewSettings = battlePassRewardViewSettings;

            _icon.sprite = rewardInfo.Icon;
            _amountText.text = rewardInfo.Value.ToString();

            if (type == BattlePassLineData.RewardType.Classic) _currentUsedButton = _classicClaimButton;
            else _currentUsedButton = _paidClaimButton;

            _currentUsedButton.onClick.AddListener(ClaimReward);

            AdditionalAdjust(rewardInfo);
        }
        protected virtual void AdditionalAdjust(BattlePassRewardInfo rewardInfo) {}

        public virtual void ClaimReward()
        {

        }

        #endregion
    }
}