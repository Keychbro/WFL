using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Product Chest Panel Info", menuName = "WOFL/Products/Product Chest Panel Info", order = 1)]
    public class ProductChestPanelInfo : ProductPanelInfo
    {
        #region Variables

        [Header("Chest Settings")]
        [SerializeField] private ChestManager.ChestType _chestType;

        #endregion

        #region Properties

        public ChestManager.ChestType ChestType { get => _chestType; }

        #endregion
    }
}