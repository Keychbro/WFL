using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using WOFL.Game;

namespace WOFL.UI
{
    public class EndGameRewardView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private TextMeshProUGUI _rewardAmount;
        [SerializeField] private Image _diamondPassView;

        #endregion

        #region Control Methods

        public void Initialize(EndGameRewardInfo rewardInfo)
        {
            _rewardAmount.text = rewardInfo.Amount.ToString();
            _rewardAmount.color = rewardInfo.RewardSettings.TextColor;

            if (rewardInfo.RewardSettings.IsDiamondPass) _diamondPassView.gameObject.SetActive(true);
            else _diamondPassView.gameObject.SetActive(false);

            _rewardIcon.sprite = rewardInfo.RewardSettings.Icon;
            _rewardIcon.rectTransform.sizeDelta = rewardInfo.RewardSettings.IconSize;
            _rewardIcon.transform.localPosition = rewardInfo.RewardSettings.IconPosition;
        }

        #endregion
    }
}