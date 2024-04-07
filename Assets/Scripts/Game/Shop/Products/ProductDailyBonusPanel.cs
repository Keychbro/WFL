using UnityEngine;
using Kamen.DataSave;
using WOFL.Settings;
using TMPro;

namespace WOFL.UI
{
    public class ProductDailyBonusPanel : ProductPanel
    {
        #region Variables

        [Header("DailyBonus Objects")]
        [SerializeField] private TextMeshProUGUI _bonusFactorText;

        [Header("DailyBonus Settings")]
        [SerializeField] private string _factorSign;

        [Header("DailyBonus Variables")]
        private int _bonusFactor;

        #endregion

        #region Control Methods

        public override void Initialize(ProductPanelInfo productPanelInfo)
        {
            base.Initialize(productPanelInfo);
            AdjustDialyBonusPanel(productPanelInfo);
        }
        private void AdjustDialyBonusPanel(ProductPanelInfo info)
        {
            if (info is ProductDailyBonusPanelInfo bonusInfo)
            {
                _bonusFactor = bonusInfo.BonusFactor;
                _bonusFactorText.text = _factorSign + _bonusFactor;
            }
            else _bonusFactorText.text = "None";
        }
        public override void GetReward()
        {
            base.GetReward();
            DataSaveManager.Instance.MyData.DailyBonusFactor = _bonusFactor;
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}