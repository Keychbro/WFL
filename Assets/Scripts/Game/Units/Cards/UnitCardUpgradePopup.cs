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

namespace WOFL.UI
{
    public class UnitCardUpgradePopup : Popup
    {
        #region Classes

        [Serializable] private class UnitStats
        {
            #region UnitStats Variables

            [SerializeField] private TextMeshProUGUI _healthAmount;
            [SerializeField] private TextMeshProUGUI _damageAmount;
            [SerializeField] private TextMeshProUGUI _manaPriceAmount;

            #endregion

            #region UnitStats Properties

            public TextMeshProUGUI HealthAmount { get => _healthAmount; }
            public TextMeshProUGUI DamageAmount { get => _damageAmount; }
            public TextMeshProUGUI ManaPriceAmount { get => _manaPriceAmount; }

            #endregion

            #region Control Methods

            public void UpdateStats(UnitLevelInfo levelInfo)
            {
                _healthAmount.text = $"{levelInfo.MaxHealthValue}";
                _damageAmount.text = $"{levelInfo.WeaponInfo.Damage}";
                _manaPriceAmount.text = $"{levelInfo.ManaPrice}";
            }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
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

        [Header("Variables")]
        private UnitDataForSave _unitData;
        private UnitLevelsHolder _levelsHolder;
        private Skin _currentSkin;

        #endregion

        #region Properties




        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();
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

            _upgradeButton.ChangeInteractable(_levelsHolder.Levels[unitData.CurrentLevel].AmountGoldToUpgrade <= DataSaveManager.Instance.MyData.Gold &&
                _levelsHolder.Levels[unitData.CurrentLevel].AmountCardToUpgrade <= _levelsHolder.Levels[_unitData.CurrentLevel].AmountCardToUpgrade);
        }
        public void UpgradeUnit()
        {
            
        }

        #endregion
    }
}