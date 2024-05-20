using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace WOFL.Game
{
    public class Broadsword : Weapon
    {
        #region Control Methods

        public override async UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            float attackTime = 100f / _currentWeaponLevel.AttackSpeed;
            await Task.Delay(Mathf.RoundToInt(attackTime * _finishAttackPoint * 1000));

            if (!_isCanAttack) return false;
            target.TakeDamage(_currentWeaponLevel.Damage);
            await Task.Delay(Mathf.RoundToInt(attackTime * (1 - _finishAttackPoint) * 1000));
            return true;
        }

        #endregion
    }
}