using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Payment
{
    [CreateAssetMenu(fileName = "Payment Info", menuName = "WOFL/Payment/PaymentInfo", order = 1)]
    public class PaymentInfo : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private PaymentManager.PaymentType _paymentType;
        [SerializeField] private Sprite _currencyIcon;
        [SerializeField] private Color32 _currencyMainColor;

        #endregion

        #region Control Methods

        public PaymentManager.PaymentType PaymentType { get => _paymentType; }
        public Sprite CurrencyIcon { get => _currencyIcon; }
        public Color32 CurrencyMainColor { get => _currencyMainColor; }

        #endregion
    }
}