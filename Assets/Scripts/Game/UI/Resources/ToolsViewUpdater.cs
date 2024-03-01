using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.DataSave;
using WOFL.Control;

namespace WOFL.UI
{
    public class ToolsViewUpdater : CurrencyViewUpdaterBase
    {
        #region Control Methods

        protected override void Initialize()
        {
            base.Initialize();
            DataSaveManager.Instance.MyData.OnToolsAmountChanged += UpdateText;
            UpdateText();
        }

        protected override void Unsubscribe()
        {
            DataSaveManager.Instance.MyData.OnToolsAmountChanged -= UpdateText;
        }

        protected override void UpdateText() => _viewText.text = BigNumberViewConverter.Instance.Convert(DataSaveManager.Instance.MyData.Tools);

        #endregion
    }
}