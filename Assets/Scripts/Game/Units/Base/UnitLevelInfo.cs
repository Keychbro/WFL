using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Unit Level Info", menuName = "WOFL/Settings/Units/Unit Level Info", order = 1)]
    public class UnitLevelInfo : ScriptableObject
    {
        #region Variables

        [Header("Game Settings")]
        [SerializeField] protected int _maxHealthValue;
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected int _amountGoldForKill;
        [SerializeField] protected int _manePrice;

        [Header("UpgradeUnit Settings")]
        [SerializeField] protected int _amountCardToUpgrade;
        [SerializeField] protected int _amountGoldToUpgrade;

        #endregion

        #region Properties

        public int MaxHealthValue { get => _maxHealthValue; }
        public float MoveSpeed { get => _moveSpeed; }
        public int AmountGoldForKill { get => _amountGoldForKill; }
        public int ManaPrice { get => _manePrice; }

        public int AmountCardToUpgrade { get => _amountCardToUpgrade; }
        public int AmountGoldToUpgrade { get => _amountGoldToUpgrade; }

        #endregion
    }
}