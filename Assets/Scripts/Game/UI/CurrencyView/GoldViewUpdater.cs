using Kamen.DataSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.UI
{
    public class GoldViewUpdater : CurrencyViewUpdaterBase
    {
        #region Control Methods

        protected override void Initialize()
        {
            base.Initialize();
            DataSaveManager.Instance.MyData.OnGoldAmountChanged += UpdateText;
            UpdateText();
        }

        protected override void Unsubscribe()
        {
            DataSaveManager.Instance.MyData.OnGoldAmountChanged -= UpdateText;
        }

        protected override void UpdateText() => _viewText.text = DataSaveManager.Instance.MyData.Gold.ToString();

        #endregion
    }
}