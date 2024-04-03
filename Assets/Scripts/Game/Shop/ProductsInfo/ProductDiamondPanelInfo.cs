using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Product Diamond Panel Info", menuName = "WOFL/Products/Product Diamond Panel Info", order = 1)]
    public class ProductDiamondPanelInfo : ProductPanelInfo
    {
        #region Variables

        [Header("Diamond Settings")]
        [SerializeField] private int _amountDiamondsReceive;

        #endregion

        #region Properties

        public int AmountDiamondsReceive { get => _amountDiamondsReceive; }

        #endregion
    }
}