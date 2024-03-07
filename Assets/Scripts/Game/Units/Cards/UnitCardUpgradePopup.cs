using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using TMPro;
using System;

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
        [SerializeField] private Button _upgradeButton;

        #endregion

        #region Properties




        #endregion

        #region Control Methods



        #endregion
    }
}