using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.Game
{
    public class IceWeapon : Weapon
    {
        #region Variables

        [Header("Ice Variables")]
        protected IceWeaponLevelInfo _currentIceWeaponLevel;

        #endregion

        public override void Initialize(WeaponLevelInfo weaponLevel)
        {
            _currentWeaponLevel = weaponLevel;
            _currentIceWeaponLevel = (IceWeaponLevelInfo)weaponLevel;
        }
        public async override UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            if (targetObject.TryGetComponent(out ISlowdownable slow)) Slowdown(slow);
            return false;
        }
        protected virtual void Slowdown(ISlowdownable target)
        {
            target.Slow(_currentIceWeaponLevel.SlowdownIncreaseValue, _currentIceWeaponLevel.SlowdownIncreaseDuration);
        }
    }
}