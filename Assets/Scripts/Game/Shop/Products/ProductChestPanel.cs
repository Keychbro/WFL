using UnityEngine;
using Kamen.DataSave;
using WOFL.Settings;
using WOFL.Control;

namespace WOFL.UI
{
    public class ProductChestPanel : ProductPanel
    {
        #region Variables

        [Header("Chest Variables")]
        private ChestManager.ChestType _chestType;

        #endregion

        #region Control Methods

        public override void Initialize(ProductPanelInfo productPanelInfo)
        {
            base.Initialize(productPanelInfo);
            AdjustChestPanel(productPanelInfo);
        }
        private void AdjustChestPanel(ProductPanelInfo info)
        {
            if (info is ProductChestPanelInfo chestInfo)
            {
                _chestType = chestInfo.ChestType;
            }
        }
        public override void GetReward()
        {
            base.GetReward();
            DataSaveManager.Instance.MyData.AddChest(_chestType);
        }

        #endregion
    }
}

