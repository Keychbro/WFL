using Kamen;
using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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
            RealMoney,
            ZombiToken
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
        public void BuyWithGold(int price, Action successfulCallback)
        {
            if (price > DataSaveManager.Instance.MyData.Gold) return;

            DataSaveManager.Instance.MyData.Gold -= price;
            DataSaveManager.Instance.SaveData();

            successfulCallback?.Invoke();
        }
        public void BuyWithDiamonds(int price, Action successfulCallback)
        {
            if (price > DataSaveManager.Instance.MyData.Diamonds) return;

            DataSaveManager.Instance.MyData.Diamonds -= price;
            DataSaveManager.Instance.SaveData();

            successfulCallback?.Invoke();
        }
        public void BuyWithTools(int price, Action successfulCallback)
        {
            if (price > DataSaveManager.Instance.MyData.Tools) return;

            DataSaveManager.Instance.MyData.Tools -= price;
            DataSaveManager.Instance.SaveData();

            successfulCallback?.Invoke();
        }
        public void BuyWithRealMoney(int price, Action successfulCallback)
        {
            //TODO IAP Buy

            successfulCallback?.Invoke();
        }
        public UnityAction<int, Action> GetBuyMethodByType(PaymentType type)
        {
            return type switch
            {
                PaymentType.Gold => BuyWithGold,
                PaymentType.Diamonds => BuyWithDiamonds,
                PaymentType.Tools => BuyWithTools,
                PaymentType.RealMoney => BuyWithRealMoney,
                _ => BuyWithGold,
            };
        }

        #endregion
    }
}