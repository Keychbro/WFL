using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace WOFL.Game
{
    public class Bow : Weapon
    {
        [Header("Settings")]
        [SerializeField] private Arrow _arrow;
        [SerializeField] private GameObject _shotPoint;

        #region Control Methods

        public override async Task<bool> DoAttack(IDamageable target)
        {
            Debug.LogError(123123);
            float attackTime = 100f / _currentWeaponLevel.AttackSpeed;
            await Task.Delay(Mathf.RoundToInt(attackTime * _finishAttackPoint * 1000));
            Debug.LogError(1233);
            Debug.LogError(_isCanAttack);
            if (!_isCanAttack) return false;
            StartCoroutine(Shot(target));
            Debug.LogError(123311);
            await Task.Delay(Mathf.RoundToInt(attackTime * (1 - _finishAttackPoint) * 1000));
            return true;
        }
        private IEnumerator Shot(IDamageable target)
        {
            Arrow arrow = Instantiate(_arrow);
            arrow.transform.position = _shotPoint.transform.position;
            Debug.LogError(Vector3.Distance(arrow.transform.position, target.HitPoint.position));
            while (Vector3.Distance(arrow.transform.position, target.HitPoint.position) > 0.1f)
            {
                if (target.HitPoint == null)
                {
                    Destroy(arrow.gameObject);
                    break;
                }
                arrow.Move(target.HitPoint);
                yield return null;
            }
           
            if (arrow != null)
            {
                target.TakeDamage(_currentWeaponLevel.Damage);
                Destroy(arrow.gameObject);
            }
        }

        #endregion
    }
}