using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using WOFL.Game;
using WOFL.Control;
using Cysharp.Threading.Tasks;
using Kamen.DataSave;
using System.Threading.Tasks;
using WOFL.Settings;
using System.Linq;

namespace WOFL.UI
{
    public class UpgradeScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private UpgradeCardsHolder _upgradeCardsHolder;
        [SerializeField] private UpgradeCastleCardView[] _upgradeCastleCardViews;

        [Header("Settings")]
        [SerializeField] private UpgradeCastleCardLevelsHolder[] _upgradeCastleCardLevelsHolders;

        #endregion

        #region Contorl Methods

        public async override void Initialize()
        {
            base.Initialize();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            await UniTask.WaitUntil(() => DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None);
            await Task.Delay(100);

            _upgradeCardsHolder.Initialize(FractionManager.Instance.CurrentFraction.Units);
            DataSaveManager.Instance.MyData.AdjustUpgradeCastleCardDatas(_upgradeCastleCardLevelsHolders);
            DataSaveManager.Instance.SaveData();

            for (int i = 0; i < _upgradeCastleCardViews.Length; i++)
            {
                _upgradeCastleCardViews[i].Initialize(
                    _upgradeCastleCardLevelsHolders.First(upgradeCastleCardLevelHolder => upgradeCastleCardLevelHolder.Type == _upgradeCastleCardViews[i].Type),
                    DataSaveManager.Instance.MyData.GetUpgradeCastleCardDataByType(_upgradeCastleCardViews[i].Type));
            }
        }

        #endregion
    }
}