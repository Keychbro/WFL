using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Upgrade Castle Card Level", menuName = "WOFL/Castle/Upgrade Castle Card Level", order = 1)]
    public class UpgradeCastleCardLevel : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _value;
        [SerializeField] private int _levelUpPrice;

        #endregion

        #region Properties

        public float Value { get => _value; }
        public int LevelUpPrice { get => _levelUpPrice; }

        #endregion
    }
}