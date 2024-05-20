using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Game;

namespace WOFL.Game
{
    public class FireAxe : FireWeapon
    {
        #region Classes

        public override async UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            float attackTime = 100f / _currentWeaponLevel.AttackSpeed;
            await UniTask.Delay(Mathf.RoundToInt(attackTime * _finishAttackPoint * 1000));

            if (!_isCanAttack) return false;
            target.TakeDamage(_currentWeaponLevel.Damage);
            GameManager.Instance.CallTakeDamageInPointWithRadius(target.SideName, targetObject.transform.position, _currentFireWeaponLevel.BurnRadius, _currentWeaponLevel.Damage);
            if (targetObject.TryGetComponent(out IFlammable flammable)) Flame(flammable);

            await UniTask.Delay(Mathf.RoundToInt(attackTime * (1 - _finishAttackPoint) * 1000));
            return true;
        }

        #endregion
    }
}