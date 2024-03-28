using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.DataSave;
using WOFL.Control;
using Cysharp.Threading.Tasks;

namespace WOFL.UI
{
    public class DiamondsViewUpdater : CurrencyViewUpdaterBase
    {
        #region Control Methods

        protected async override void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);

            DataSaveManager.Instance.MyData.OnDiamondsAmountChanged += UpdateText;
            UpdateText();
        }
        protected override void Unsubscribe()
        {
            DataSaveManager.Instance.MyData.OnDiamondsAmountChanged -= UpdateText;
        }
        protected override void UpdateText() => _viewText.text = BigNumberViewConverter.Instance.Convert(DataSaveManager.Instance.MyData.Diamonds);

        #endregion
    }
}

