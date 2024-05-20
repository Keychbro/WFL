using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WOFL.Settings;


namespace WOFL.Game
{
    public class FireWeapon : Weapon
    {
        #region Variables

        [Header("Ice Variables")]
        protected FireWeaponLevelInfo _currentFireWeaponLevel;

        #endregion

        public override void Initialize(WeaponLevelInfo weaponLevel)
        {
            _currentWeaponLevel = weaponLevel;
            _currentFireWeaponLevel = (FireWeaponLevelInfo)weaponLevel;
        }
        public async override UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            if (targetObject.TryGetComponent(out IFlammable flammable)) Flame(flammable);
            return false;
        }
        protected virtual void Flame(IFlammable target)
        {
            target.Burn(_currentFireWeaponLevel.FireDamagePerSeconds, _currentFireWeaponLevel.BurningDuration);
        }
    }
}