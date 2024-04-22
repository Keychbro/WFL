using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Payment;
using WOFL.Settings;
using WOFL.DataSave;
using UnityEngine.Events;
using System;
using Kamen.DataSave;

namespace WOFL.UI
{
    public class UpgradeCastleCardView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _cardName;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private PaymentView _paymentView;

        [Header("Settings")]
        [SerializeField] private UpgradeCastleCardLevelsHolder.UpgradeCastleCardType _type;
        [SerializeField] private string _levelName;
        [SerializeField] private string _additionalTextForValue;

        [Header("Variables")]
        private UpgradeCastleCardLevelsHolder _currentUpgradeCastleCardLevelHolder;
        private UpgradeCastleCardData _currentUpgradeCastleCardData;
        protected event UnityAction<int, Action> PaymentMethod;

        #endregion

        #region Control Methods

        public UpgradeCastleCardLevelsHolder.UpgradeCastleCardType Type { get => _type; }

        #endregion

        #region Control Methods

        public void Initialize(UpgradeCastleCardLevelsHolder upgradeCastleCardLevelHolder, UpgradeCastleCardData upgradeCastleCardData)
        {
            _currentUpgradeCastleCardLevelHolder = upgradeCastleCardLevelHolder;
            _currentUpgradeCastleCardData = upgradeCastleCardData;

            int currentLevel = _currentUpgradeCastleCardData.Level < _currentUpgradeCastleCardLevelHolder.CardLeveles.Length ? _currentUpgradeCastleCardData.Level : _currentUpgradeCastleCardLevelHolder.CardLeveles.Length - 1;
            _cardName.text = $"{_currentUpgradeCastleCardLevelHolder.Name}";
            UpdateLevelView();
            AdjustPaymentButton(currentLevel);
            _currentUpgradeCastleCardData.OnLevelChanged += UpdateLevelView;
        }
        private void AdjustPaymentButton(int currentLevel)
        {
            PaymentMethod = PaymentManager.Instance.GetBuyMethodByType(_paymentView.ChoosenType);

            _paymentView.Initialize(
                PaymentManager.Instance.GetPaymentInfoByType(_paymentView.ChoosenType), 
                _currentUpgradeCastleCardLevelHolder.CardLeveles[currentLevel].LevelUpPrice.ToString());

            _paymentView.OnTryPay.AddListener(() =>
            {
                PaymentMethod?.Invoke(0, LevelUp);
            });
        }
        public void LevelUp()
        {
            _currentUpgradeCastleCardData.IncreaseLevel();
            DataSaveManager.Instance.SaveData();
        }
        private void UpdateLevelView()
        {
            Debug.Log(_currentUpgradeCastleCardData.Level);
            string levelValue = _currentUpgradeCastleCardData.Level < _currentUpgradeCastleCardLevelHolder.CardLeveles.Length ? _currentUpgradeCastleCardData.Level.ToString() : "MAX";
            int currentLevel = _currentUpgradeCastleCardData.Level < _currentUpgradeCastleCardLevelHolder.CardLeveles.Length ? _currentUpgradeCastleCardData.Level : _currentUpgradeCastleCardLevelHolder.CardLeveles.Length - 1;
            _levelText.text = $"{_levelName} {levelValue}";
            _valueText.text = $"{_currentUpgradeCastleCardLevelHolder.CardLeveles[currentLevel].Value}{_additionalTextForValue}";
            _paymentView.UpdatePriceText(_currentUpgradeCastleCardLevelHolder.CardLeveles[currentLevel].LevelUpPrice.ToString());
        }

        #endregion
    }
}