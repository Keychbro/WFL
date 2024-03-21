using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using TMPro;
using System;
using WOFL.Settings;
using WOFL.Game;
using WOFL.Control;
using WOFL.Save;
using Kamen.DataSave;
using Unity.VisualScripting;
using Cysharp.Threading.Tasks;

namespace WOFL.UI
{
    public class UnitCardUpgradePopup : Popup
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private UpgradeCardsHolder _upgradeCardHolder;
        [Space]
        [SerializeField] private TextMeshProUGUI _levelText;
        [Space]
        [SerializeField] private Image _fractionNameBackground;
        [SerializeField] private TextMeshProUGUI _fractionNameText;
        [Space]
        [SerializeField] private Image _unitView;
        [SerializeField] private UnitStats _currentStats;
        [SerializeField] private UnitStats _nextStats;
        [Space]
        [SerializeField] private KamenButton _upgradeButton;
        [SerializeField] private TextMeshProUGUI _upgradePrice;

        [Header("Variables")]
        private UnitDataForSave _unitData;
        private UnitLevelsHolder _levelsHolder;
        private Skin _currentSkin;

        #endregion

        #region Control Methods

        public async override void Initialize()
        {
            base.Initialize();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);

            _upgradeCardHolder.SubscribeOnCardsMoreButton(AdjustStats);
            _upgradeButton.Initialize();
            _upgradeButton.OnClick().AddListener(UpgradeUnit);
        }
        public void AdjustStats(UnitDataForSave unitData, UnitLevelsHolder levelsHolder, Skin currentSkin)
        {
            _unitData = unitData;
            _levelsHolder = levelsHolder;
            _currentSkin = currentSkin;

            _levelText.text = $"LEVEl {unitData.CurrentLevel}";
            (_fractionNameText.text, _fractionNameBackground.color) = FractionManager.Instance.GetCurrentFractionAtributes();

            _unitView.sprite = currentSkin.SkinSprite;
            _currentStats.UpdateStats(_levelsHolder.Levels[unitData.CurrentLevel]);
            _nextStats.UpdateStats(_levelsHolder.Levels[unitData.CurrentLevel + 1]);

            _upgradeButton.ChangeInteractable(CheckOpportunityToBuy());
            _upgradePrice.text = $"{_levelsHolder.Levels[_unitData.CurrentLevel].AmountGoldToUpgrade}";
        }
        public void UpgradeUnit()
        {
            if (!CheckOpportunityToBuy()) return;

            DataSaveManager.Instance.MyData.Gold -= _levelsHolder.Levels[_unitData.CurrentLevel].AmountGoldToUpgrade;
            _unitData.AmountCards -= _levelsHolder.Levels[_unitData.CurrentLevel].AmountCardToUpgrade;
            _unitData.IncreaseLevel();

            DataSaveManager.Instance.SaveData();
            AdjustStats(_unitData, _levelsHolder, _currentSkin);
        }
        private bool CheckOpportunityToBuy()
        {
            return DataSaveManager.Instance.MyData.Gold >= _levelsHolder.Levels[_unitData.CurrentLevel].AmountGoldToUpgrade &&
                _unitData.AmountCards >= _levelsHolder.Levels[_unitData.CurrentLevel].AmountCardToUpgrade;
        }

        #endregion
    }
}