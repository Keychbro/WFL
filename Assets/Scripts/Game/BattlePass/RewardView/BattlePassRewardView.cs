using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.UI;
using WOFL.Settings;
using System.Linq;
using Kamen.DataSave;
using System;

namespace WOFL.BattlePass
{
    public class BattlePassRewardView : MonoBehaviour
    {
        #region Classes

        [Serializable] protected class RewardButtonInfo
        {
            #region RewardButtonInfo Variables

            [SerializeField] private Button _button;
            [SerializeField] private Image _background;
            [SerializeField] private Image _icon;

            #endregion

            #region RewardButtonInfo Properties

            public Button Button { get => _button; }
            public Image Background { get => _background; }
            public Image Icon { get => _icon; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] protected Image _background;
        [SerializeField] protected Image _icon;
        [SerializeField] protected TextMeshProUGUI _amountText;
        [SerializeField] protected RewardStateView _rewardState;
        [Space]
        [SerializeField] protected RewardButtonInfo _classicClaimButton;
        [SerializeField] protected RewardButtonInfo _paidClaimButton;

        [Header("Variables")]
        protected BattlePassRewardInfo _rewardInfo;
        protected BattlePassRewardViewSettings _rewardViewSettings;
        protected BattlePassDataSave.RewardStateData _rewardStateData;
        protected RewardButtonInfo _currentUsedButton;

        #endregion

        #region Control Methods

        public virtual void Initialize(BattlePassRewardInfo rewardInfo, BattlePassRewardViewSettings battlePassRewardViewSettings, BattlePassDataSave.RewardStateData rewardStateData)
        {
            _rewardInfo = rewardInfo;
            _rewardViewSettings = battlePassRewardViewSettings;
            _rewardStateData = rewardStateData;

            _icon.sprite = _rewardInfo.Icon;
            _amountText.text = _rewardInfo.Value.ToString();

            if (battlePassRewardViewSettings.Type == BattlePassRewardViewSettings.RewardType.Classic) _currentUsedButton = _classicClaimButton;
            else _currentUsedButton = _paidClaimButton;
            _currentUsedButton.Button.onClick.AddListener(ClaimReward);
            _currentUsedButton.Background.color = _rewardViewSettings.ViewSettingsInfos.First(viewSettingsInfo => viewSettingsInfo.State == RewardStateView.RewardState.Ready).Color;

            DataSaveManager.Instance.MyData.OnAdsRemoved += SetViewByState;
            _rewardStateData.OnRewardStateChanged += SetViewByState;
            SetViewByState();

            AdditionalAdjust(_rewardInfo);
        }
        protected virtual void AdditionalAdjust(BattlePassRewardInfo rewardInfo) {}
        private void SetViewByState()
        {
            if (_rewardViewSettings.Type == BattlePassRewardViewSettings.RewardType.ForPaid && !DataSaveManager.Instance.MyData.IsAdsRemoved)
            {
                SwitchState(RewardStateView.RewardState.Closed);
                return;
            }

            SwitchState(_rewardStateData.RewardState);
            switch (_rewardStateData.RewardState)
            {
                case RewardStateView.RewardState.Closed:
                    _currentUsedButton.Button.gameObject.SetActive(false);
                    break;
                case RewardStateView.RewardState.Ready:
                    _currentUsedButton.Button.gameObject.SetActive(true);
                    break;
                case RewardStateView.RewardState.Accepted:
                    _currentUsedButton.Button.gameObject.SetActive(false);
                    break;
            }
        }
        private void SwitchState(RewardStateView.RewardState state)
        {
            _rewardState.SwitchState(state);
            _background.color = _rewardViewSettings.ViewSettingsInfos.First(viewSettingsInfo => viewSettingsInfo.State == state).Color;
        }
        public virtual void ClaimReward()
        {
            if (_rewardStateData.RewardState != RewardStateView.RewardState.Ready) return;

            if (_rewardInfo is BattlePassRewardGoldInfo)
            {
                DataSaveManager.Instance.MyData.Gold += _rewardInfo.Value;
            }
            else if (_rewardInfo is BattlePassRewardDiamondsInfo)
            {
                DataSaveManager.Instance.MyData.Diamonds += _rewardInfo.Value;
            }
            else if (_rewardInfo is BattlePassRewardToolsInfo)
            {
                DataSaveManager.Instance.MyData.Diamonds += _rewardInfo.Value;
            }
            else if (_rewardInfo is BattlePassRewardCardsInfo)
            {
                BattlePassRewardCardsInfo.RewardCardInfo cardInfo = ((BattlePassRewardCardsInfo)_rewardInfo).RewardCardInfos.
                    First(rewardCardInfo => rewardCardInfo.FractionName == DataSaveManager.Instance.MyData.ChoosenFraction);

                DataSaveManager.Instance.MyData.GetUnitDataMyName(cardInfo.UniqueName).AmountCards += _rewardInfo.Value;
            }

            _rewardStateData.NextRewardState();
            DataSaveManager.Instance.SaveData();         
        }

        #endregion
    }
}