using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Product Daily Bonus Panel Info", menuName = "WOFL/Products/Product Daily Bonus Panel Info",order = 1)]
    public class ProductDailyBonusPanelInfo : ProductPanelInfo
    {
        #region Variables

        [Header("Daily Bonus Settings")]
        [SerializeField] private int _bonusFactor;

        #endregion

        #region Properties

        public int BonusFactor { get => _bonusFactor; }

        #endregion
    }
}