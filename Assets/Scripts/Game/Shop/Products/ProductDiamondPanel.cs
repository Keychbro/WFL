using Kamen.DataSave;
using TMPro;
using UnityEngine;
using WOFL.Settings;
using WOFL.Control;

namespace WOFL.UI
{
    public class ProductDiamondPanel : ProductPanel
    {
        #region Variables

        [Header("Diamond Settings")]
        [SerializeField] private TextMeshProUGUI _amountDiamondsReceiveText;

        [Header("Diamond Variables")]
        private int _amountDiamondsReceive;

        #endregion

        #region Control Methods

        public override void Initialize(ProductPanelInfo productPanelInfo)
        {
            base.Initialize(productPanelInfo);
            AdjustDiamondPanel(productPanelInfo);
        }
        private void AdjustDiamondPanel(ProductPanelInfo info)
        {
            if (info is ProductDiamondPanelInfo diamondInfo)
            {
                _amountDiamondsReceive = diamondInfo.AmountDiamondsReceive;
                _amountDiamondsReceiveText.text = BigNumberViewConverter.Instance.Convert(_amountDiamondsReceive).ToString();
            }
            else _amountDiamondsReceiveText.text = "None";
        }
        public override void GetReward()
        {
            base.GetReward();
            DataSaveManager.Instance.MyData.Diamonds += _amountDiamondsReceive;
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}