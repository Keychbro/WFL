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

        public void UpdateStats(UnitLevelInfo levelInfo)
        {
            _healthAmount.text = $"{levelInfo.MaxHealthValue}";
            _damageAmount.text = $"{levelInfo.WeaponInfo.Damage}";
            _manaPriceAmount.text = $"{levelInfo.ManaPrice}";
        }

        #endregion
    }
}