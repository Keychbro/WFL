using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Fire Weapon Level Info", menuName = "WOFL/Settings/Units/Fire Weapon Level Info", order = 1)]
    public class FireWeaponLevelInfo : WeaponLevelInfo
    {
        #region Variables

        [Header("Fire Settings")]
        [SerializeField] private int _fireDamagePerSeconds;
        [SerializeField] private int _burningDuration;
        [SerializeField] private float _burnRadius;

        #endregion

        #region Propeties

        public int FireDamagePerSeconds { get => _fireDamagePerSeconds; }
        public int BurningDuration { get => _burningDuration; }
        public float BurnRadius { get => _burnRadius; } 

        #endregion
    }
}