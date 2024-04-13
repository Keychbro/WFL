using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WOFL.IAP;
using WOFL.Payment;
using WOFL.Settings;

namespace WOFL.DiamondPass
{
    public class DiamondPassView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _ticket;
        [SerializeField] private Image _icon;
        [Space]
        [SerializeField] private PaymentView _paymentView;
        [Space]
        [SerializeField] private Slider _levelsBar;
        [SerializeField] private TextMeshProUGUI _stageText;
        [SerializeField] private TextMeshProUGUI _progressText;

        [Header("Settings")]
        [SerializeField] private string _priceName;
        [SerializeField] private DiamondPassTicketSettings _diamondPassTicketSettings;

        [Header("Variables")]
        private DiamondPassStageInfo[] _stageInfos;
        private DiamondPassDataSave _passDataSave;

        private DiamondPassDataSave.StageData _currentStageData;
        private DiamondPassStageInfo _currentStageInfo;

        protected event UnityAction<int, Action> PaymentMethod;

        #endregion

        #region Control Methods

        public void Initialize(DiamondPassStageInfo[] stageInfos, DiamondPassDataSave passDataSave)
        {
            _stageInfos = stageInfos;
            _passDataSave = passDataSave;

            _passDataSave.OnPurchased += UpdatePassMode;
            UpdatePassMode();

            DataSaveManager.Instance.MyData.OnGameLevelChanged += UpdateStageView;
            UpdateCurrentStage();
            AdjustPaymentButton();
        }
        private void AdjustPaymentButton()
        {
            PaymentMethod = PaymentManager.Instance.GetBuyMethodByType(_paymentView.ChoosenType);

            _paymentView.Initialize(
                PaymentManager.Instance.GetPaymentInfoByType(_paymentView.ChoosenType),
                IAPManager.Instance.GetPriceByName(_priceName));

            _paymentView.OnTryPay.AddListener(() =>
            {
                PaymentMethod?.Invoke(0, ReceivePass);
            });
        }
        private void UpdateCurrentStage()
        {
            if (_currentStageData != null) _currentStageData.OnRewardReceived -= UpdateCurrentStage;

            _currentStageInfo = _stageInfos.First(stageInfo => stageInfo.TargetLevel > DataSaveManager.Instance.MyData.GameLevel);
            _currentStageData = _passDataSave.GetStageInfoByLevel(_currentStageInfo.TargetLevel);

            _currentStageData.OnRewardReceived += UpdateCurrentStage;
            _levelsBar.maxValue = _currentStageInfo.TargetLevel;
            _stageText.text = $"Stage {Array.FindIndex(_stageInfos ,stageInfo => stageInfo.TargetLevel == _currentStageInfo.TargetLevel)}";

            UpdateStageView();
        }
        private void UpdateStageView()
        {
            _levelsBar.value = DataSaveManager.Instance.MyData.GameLevel;
            _progressText.text = $"{DataSaveManager.Instance.MyData.GameLevel}/{_currentStageInfo.TargetLevel}";
        }
        private void UpdatePassMode()
        {
            if (_passDataSave.IsPassPurchased)
            {
                _ticket.sprite = _diamondPassTicketSettings.OnTicket;
                _paymentView.gameObject.SetActive(false);
                _levelsBar.gameObject.SetActive(true);
                _icon.gameObject.SetActive(true);
            }
            else
            {
                _ticket.sprite = _diamondPassTicketSettings.OffTicket;
                _paymentView.gameObject.SetActive(true);
                _levelsBar.gameObject.SetActive(false);
                _icon.gameObject.SetActive(false);
            }
        }
        public void ReceivePass()
        {
            _passDataSave.PurchasePass();
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}