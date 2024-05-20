using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Weapon Level Info", menuName = "WOFL/Settings/Units/Weapon Level Info", order = 1)]
    public class WeaponLevelInfo : ScriptableObject
    {
        #region Variables

        [SerializeField] protected int _damage;
        [SerializeField] protected float _attackSpeed;
        [SerializeField] protected float _attackRange;

        #endregion

        #region Properties

        public int Damage { get => _damage; }
        public float AttackSpeed { get => _attackSpeed; }
        public float AttackRange { get => _attackRange; }

        #endregion
    }
}