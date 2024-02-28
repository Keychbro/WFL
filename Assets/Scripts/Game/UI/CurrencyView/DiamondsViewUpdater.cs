using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.DataSave;

namespace WOFL.UI
{
    public class DiamondsViewUpdater : CurrencyViewUpdaterBase
    {
        #region Control Methods

        protected override void Initialize()
        {
            base.Initialize();
            DataSaveManager.Instance.MyData.OnDiamondsAmountChanged += UpdateText;
            UpdateText();
        }
        protected override void Unsubscribe()
        {
            DataSaveManager.Instance.MyData.OnDiamondsAmountChanged -= UpdateText;
        }
        protected override void UpdateText() => _viewText.text = DataSaveManager.Instance.MyData.Diamonds.ToString();

        #endregion
    }
}

