using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.UI;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Product Panel Info", menuName = "WOFL/Products/Product Panel Info", order = 1)]
    public class ProductPanelInfo : ScriptableObject
    {
        #region Variables

        [Header("Main Settings")]
        [SerializeField] private string _name;

        [Header("Icon Settings")]
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector2 _iconSize;
        [SerializeField] private Vector3 _iconPosition;

        [Header("Additional Settings")]
        [SerializeField] private ProductGoodOfferView.OfferType _offerType;

        #endregion

        #region Properties

        public string Name { get => _name; }
        public Sprite Icon { get => _icon; }
        public Vector2 IconSize { get => _iconSize; }
        public Vector3 IconPosition { get => _iconPosition; }
        public ProductGoodOfferView.OfferType OfferType { get => _offerType; }

        #endregion
    }
}