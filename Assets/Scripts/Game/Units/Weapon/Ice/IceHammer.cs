using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public class IceHammer : IceWeapon
    {
        #region Classes

        public override async UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            float attackTime = 100f / _currentWeaponLevel.AttackSpeed;
            await UniTask.Delay(Mathf.RoundToInt(attackTime * _finishAttackPoint * 1000));

            if (!_isCanAttack) return false;
            target.TakeDamage(_currentWeaponLevel.Damage);
            if (targetObject.TryGetComponent(out ISlowdownable slow))
            {
                if (slow.SpeedIncreaseValue < 1) target.TakeDamage(_currentWeaponLevel.Damage);
                Slowdown(slow);
            }
            await UniTask.Delay(Mathf.RoundToInt(attackTime * (1 - _finishAttackPoint) * 1000));
            return true;
        }

        #endregion
    }
}