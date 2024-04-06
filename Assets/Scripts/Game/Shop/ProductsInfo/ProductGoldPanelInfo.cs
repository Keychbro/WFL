using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Product Gold Panel Info", menuName = "WOFL/Products/Product Gold Panel Info", order = 1)]
    public class ProductGoldPanelInfo : ProductPanelInfo
    {
        #region Variables

        [Header("Gold Settings")]
        [SerializeField] private int _amountGoldReceive;

        #endregion

        #region Properties

        public int AmountGoldReceive { get => _amountGoldReceive; }

        #endregion
    }
}