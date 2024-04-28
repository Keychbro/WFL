using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.Game
{
    public abstract class Weapon : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField][Range(0f, 1f)] protected float _finishAttackPoint;

        [Header("Variables")]
        protected WeaponLevelInfo _currentWeaponLevel;
        protected bool _isCanAttack = true;

        #endregion

        #region Control Methods

        public void Initialize(WeaponLevelInfo weaponLevel)
        {
            _currentWeaponLevel = weaponLevel;
        }
        public abstract Task<bool> DoAttack(IDamageable target);
        public void StopAttack() => _isCanAttack = false;

        #endregion
    }
}