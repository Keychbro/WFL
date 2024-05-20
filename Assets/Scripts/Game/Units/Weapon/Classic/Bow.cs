using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WOFL.Control;

namespace WOFL.Game
{
    public class Bow : Weapon
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Arrow _arrow;
        [SerializeField] private GameObject _shotPoint;

        #endregion

        #region Control Methods

        public override async UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            float attackTime = 100f / _currentWeaponLevel.AttackSpeed;
            await Task.Delay(Mathf.RoundToInt(attackTime * _finishAttackPoint * 1000));
            if (!_isCanAttack) return false;
            StartCoroutine(Shot(target));
            await Task.Delay(Mathf.RoundToInt(attackTime * (1 - _finishAttackPoint) * 1000));
            return true;
        }
        private IEnumerator Shot(IDamageable target)
        {
            Arrow arrow = Instantiate(_arrow, BackgroundMover.Instance.transform);
            arrow.transform.position = _shotPoint.transform.position;
            while (target != null && Vector3.Distance(arrow.transform.position, target.HitPoint.position) > 0.1f)
            {
                if (target.HitPoint == null)
                {
                    Destroy(arrow.gameObject);
                    break;
                }
                arrow.Move(target.HitPoint);
                yield return null;
            }

            if (target != null)
            {
                target.TakeDamage(_currentWeaponLevel.Damage);
                Destroy(arrow.gameObject);
            }
        }

        #endregion
    }
}