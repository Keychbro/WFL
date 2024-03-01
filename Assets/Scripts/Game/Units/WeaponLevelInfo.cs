using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Weapon Level Info", menuName = "WOFL/Settings/Units/Weapon Level Info", order = 1)]
    public class WeaponLevelInfo : ScriptableObject
    {
        #region Variables

        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;

        #endregion

        #region Properties

        public int Damage { get => _damage; }
        public float AttackSpeed { get => _attackSpeed; }

        #endregion
    }
}