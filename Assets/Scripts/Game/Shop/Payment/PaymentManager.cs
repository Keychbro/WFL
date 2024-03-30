using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Payment
{
    public class PaymentManager : MonoBehaviour
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
    }
}