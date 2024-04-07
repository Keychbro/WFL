using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.DataSave;
using WOFL.Settings;

namespace WOFL.UI
{
    public class ProductRemoveAdsPanel : ProductPanel
    {
        #region Control Methods

        public override void GetReward()
        {
            base.GetReward();
            DataSaveManager.Instance.MyData.IsAdsRemoved = true;
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}