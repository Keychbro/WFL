using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using WOFL.Control;
using WOFL.Settings;
using WOFL.Payment;
using WOFL.IAP;
using System;

namespace WOFL.UI
{
    public class ProductPanel : MonoBehaviour
    {
        #region Variables

        [Header("Product Objects")]
        [SerializeField] protected Image _background;
        [SerializeField] protected TextMeshProUGUI _title;
        [SerializeField] protected Image _productIcon;
        [SerializeField] protected ProductGoodOfferView _goodOfferView;
        [SerializeField] protected SoonView _soonView;
        [Space]
        [SerializeField] protected PaymentView _paymentView;

        [Header("Variables")]
        protected ProductPanelInfo _currentProductPanelInfo;
        protected event UnityAction<int, Action> PaymentMethod;

        [Header("ProductPurchaseData")]
        [SerializeField] private string ProductID;

        #endregion

        #region Control Methods

        public virtual void Initialize(ProductPanelInfo productPanelInfo)
        {
            _currentProductPanelInfo = productPanelInfo;

            ProductID = productPanelInfo.productID;
            _title.text = _currentProductPanelInfo.Name;
            _productIcon.sprite = _currentProductPanelInfo.Icon;
            _productIcon.rectTransform.sizeDelta = _currentProductPanelInfo.IconSize;
            _productIcon.rectTransform.anchoredPosition = _currentProductPanelInfo.IconPosition;

            PaymentMethod = PaymentManager.Instance.GetBuyMethodByType(_currentProductPanelInfo.PaymentType);

            _paymentView.Initialize(
                PaymentManager.Instance.GetPaymentInfoByType(_currentProductPanelInfo.PaymentType),
                _currentProductPanelInfo.PaymentType == PaymentManager.PaymentType.RealMoney ? IAPManager.Instance.GetPriceByName(_currentProductPanelInfo.PriceName) : _currentProductPanelInfo.Price.ToString());

            _paymentView.OnTryPay.AddListener(() =>
            {
                PaymentMethod?.Invoke(_currentProductPanelInfo.Price, GetReward); 
            });

            if (!productPanelInfo.IsSoonViewActive) _goodOfferView.AdjustView(_currentProductPanelInfo.OfferType);
            else
            {
                _paymentView.gameObject.SetActive(false);
                _soonView.gameObject.SetActive(true);
            }
        }
        public virtual void GetReward()
        {
            //DO Nothing
            Debug.Log("Get rewarded!");
        }

        #endregion
    }
}