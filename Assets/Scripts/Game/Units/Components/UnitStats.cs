using System;
using TMPro;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.UI
{
    [Serializable] public class UnitStats
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

        public void UpdateStats(UnitLevelInfo unitLevelInfo, WeaponLevelInfo weaponLevelInfo)
        {
            _healthAmount.text = $"{(unitLevelInfo == null ? 0 : unitLevelInfo.MaxHealthValue)}";
            _damageAmount.text = $"{(weaponLevelInfo == null ? 0 : weaponLevelInfo.Damage)}";
            _manaPriceAmount.text = $"{(unitLevelInfo == null ? 0 : unitLevelInfo.ManaPrice)}";
        }

        #endregion
    }
}