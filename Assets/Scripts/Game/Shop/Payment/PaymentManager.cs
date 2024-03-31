using Kamen;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WOFL.Payment
{
    public class PaymentManager : SingletonComponent<PaymentManager>
    {
        #region Enums

        public enum PaymentType
        {
            Gold,
            Diamonds,
            Tools,
            RealMoney
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private PaymentInfo[] _paymentInfos;

        #endregion

        #region Control Methods

        public PaymentInfo GetPaymentInfoByType(PaymentType type) => _paymentInfos.First(paymentInfo => paymentInfo.PaymentType == type);
        public void TestClick()
        {

        }

        #endregion
    }
}