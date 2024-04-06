using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Product Tools Panel Info", menuName = "WOFL/Products/Product Tools Panel Info", order = 1)]
    public class ProductToolsPanelInfo : ProductPanelInfo
    {
        #region Variables

        [Header("Tools Settings")]
        [SerializeField] private int _amountToolsReceive;

        #endregion

        #region Properties

        public int AmountToolsReceive { get => _amountToolsReceive; }

        #endregion
    }
}

