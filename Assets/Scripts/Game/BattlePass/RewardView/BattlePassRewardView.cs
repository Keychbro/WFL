using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.UI;

namespace WOFL.BattlePass
{
    public class BattlePassRewardView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] protected Image _icon;
        [SerializeField] protected TextMeshProUGUI _amountText;
        [SerializeField] protected RewardStateView _rewardState;
        [SerializeField] protected Button _claimButton;

        #endregion

        #region Control Methods

        public virtual void Initialize(BattlePassRewardInfo rewardInfo)
        {
            _icon.sprite = rewardInfo.Icon;
            _amountText.text = rewardInfo.Value.ToString();
            _claimButton.onClick.AddListener(ClaimReward);

            AdditionalAdjust(rewardInfo);
        }
        protected virtual void AdditionalAdjust(BattlePassRewardInfo rewardInfo) {}

        public virtual void ClaimReward()
        {

        }

        #endregion
    }
}