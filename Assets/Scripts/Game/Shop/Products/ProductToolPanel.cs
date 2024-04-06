using UnityEngine;
using WOFL.Settings;
using WOFL.Control;
using TMPro;
using Kamen.DataSave;

namespace WOFL.UI
{
    public class ProductToolPanel : ProductPanel
    {
        #region Variables

        [Header("Tools Settings")]
        [SerializeField] private TextMeshProUGUI _amountToolsReceiveText;

        [Header("Tools Variables")]
        private int _amountToolsReceive;

        #endregion

        #region Control Methods

        public override void Initialize(ProductPanelInfo productPanelInfo)
        {
            base.Initialize(productPanelInfo);
            AdjustToolsPanel(productPanelInfo);
        }
        private void AdjustToolsPanel(ProductPanelInfo info)
        {
            if (info is ProductToolsPanelInfo toolsInfo)
            {
                _amountToolsReceive = toolsInfo.AmountToolsReceive;
                _amountToolsReceiveText.text = BigNumberViewConverter.Instance.Convert(_amountToolsReceive);
            }
            else _amountToolsReceiveText.text = "None";
        }
        public override void GetReward()
        {
            base.GetReward();
            DataSaveManager.Instance.MyData.Tools += _amountToolsReceive;
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}