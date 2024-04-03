using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.UI;
using WOFL.Payment;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Product Panel Info", menuName = "WOFL/Products/Product Panel Info", order = 1)]
    public class ProductPanelInfo : ScriptableObject
    {
        #region Variables

        [Header("Main Settings")]
        [SerializeField] protected string _name;
        [Space]
        [SerializeField] protected int _price;
        [SerializeField] protected PaymentManager.PaymentType _paymentType;

        [Header("Icon Settings")]
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected Vector2 _iconSize;
        [SerializeField] protected Vector3 _iconPosition;

        [Header("Additional Settings")]
        [SerializeField] protected ProductGoodOfferView.OfferType _offerType;

        #endregion

        #region Properties

        public string Name { get => _name; }
        public int Price { get => _price; }
        public PaymentManager.PaymentType PaymentType { get => _paymentType; }
        public Sprite Icon { get => _icon; }
        public Vector2 IconSize { get => _iconSize; }
        public Vector3 IconPosition { get => _iconPosition; }
        public ProductGoodOfferView.OfferType OfferType { get => _offerType; }

        #endregion
    }
}