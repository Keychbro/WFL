using Kamen.DataSave;
using TMPro;
using UnityEngine;
using WOFL.Control;
using WOFL.Settings;

namespace WOFL.UI
{
    public class ProductGoldPanel : ProductPanel
    {
        #region Variables

        [Header("Gold Settings")]
        [SerializeField] private TextMeshProUGUI _amountGoldReveiveText;

        [Header("Gold Variables")]
        private int _amountGoldReceive;

        #endregion

        #region Control Methods

        public override void Initialize(ProductPanelInfo productPanelInfo)
        {
            base.Initialize(productPanelInfo);
            AdjustGoldPanel(productPanelInfo);
        }
        private void AdjustGoldPanel(ProductPanelInfo info)
        {
            if (info is ProductGoldPanelInfo goldInfo)
            {
                _amountGoldReceive = goldInfo.AmountGoldReceive;
                _amountGoldReveiveText.text = BigNumberViewConverter.Instance.Convert(_amountGoldReceive).ToString();
            }
            else _amountGoldReveiveText.text = "None";
        }
        public override void GetReward()
        {
            base.GetReward();
            DataSaveManager.Instance.MyData.Gold += _amountGoldReceive;
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}