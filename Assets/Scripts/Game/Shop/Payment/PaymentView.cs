using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using WOFL.UI;
using WOFL.Control;

namespace WOFL.Payment
{
    public class PaymentView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private KamenButton _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private LoadingIcon _loadingIcon;

        [Header("Settings")]
        [SerializeField] private bool _isUseCurrencyColor;
        [SerializeField] private Color32 _newPriceColor;
        [SerializeField] private PaymentManager.PaymentType _choosenType;

        [Header("Variables")]
        private PaymentInfo _currentPaymentInfo;

        #endregion

        #region Properties

        public Button.ButtonClickedEvent OnTryPay { get => _button.OnClick(); }
        public PaymentManager.PaymentType ChoosenType { get => _choosenType; }

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _button.Initialize();
            _button.OnClick().RemoveAllListeners();
        }

        #endregion

        #region Control Methods

        public void Initialize(PaymentInfo paymentInfo, string priceText)
        {
            _currentPaymentInfo = paymentInfo;

            if (_icon != null) _icon.sprite = _currentPaymentInfo.CurrencyIcon;

            if (int.TryParse(priceText, out int priceInValue)) _price.text = BigNumberViewConverter.Instance.Convert(priceInValue).ToString();
            else _price.text = priceText;
            _price.color = _isUseCurrencyColor ? _currentPaymentInfo.CurrencyMainColor : _newPriceColor;

            _button.Initialize();
        }

        #endregion
    }
}
