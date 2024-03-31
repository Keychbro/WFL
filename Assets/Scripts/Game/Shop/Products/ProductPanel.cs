using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Control;
using WOFL.Settings;
using WOFL.Payment;

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
        [Space]
        [SerializeField] protected PaymentView _paymentView;

        [Header("Variables")]
        protected ProductPanelInfo _currentProductPanelInfo;

        #endregion

        #region Control Methods

        public void Initialize(ProductPanelInfo productPanelInfo)
        {
            _currentProductPanelInfo = productPanelInfo;

            _title.text = _currentProductPanelInfo.Name;
            _productIcon.sprite = _currentProductPanelInfo.Icon;
            _productIcon.rectTransform.sizeDelta = _currentProductPanelInfo.IconSize;
            _productIcon.rectTransform.anchoredPosition = _currentProductPanelInfo.IconPosition;

            _paymentView.Initialize(PaymentManager.Instance.GetPaymentInfoByType(_paymentView.ChoosenType), 10.ToString(), PaymentManager.Instance.TestClick); //TODO Fix price and callback
            _goodOfferView.AdjustView(_currentProductPanelInfo.OfferType);
        }

        #endregion
    }
}