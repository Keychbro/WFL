using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Game;

namespace WOFL.Game
{
    public class BloodStaff : Weapon
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private Bat _bat;

        [Header("Settings")]
        [SerializeField] private int _amountBats;
        [SerializeField] private GameObject _shotPoint;
        [SerializeField] private Vector3 _spawnBatsOffset;

        #endregion

        #region Control Methods

        public override async UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            float attackTime = 100f / _currentWeaponLevel.AttackSpeed;
            await UniTask.Delay(Mathf.RoundToInt(attackTime * _finishAttackPoint * 1000));
            if (!_isCanAttack) return false;

            for (int i = 0; i < _amountBats; i++)
            {
                StartCoroutine(Shot(target, targetObject, _currentWeaponLevel.Damage / _amountBats));
            }
;
            await UniTask.Delay(Mathf.RoundToInt(attackTime * (1 - _finishAttackPoint) * 1000));
            return true;
        }
        private IEnumerator Shot(IDamageable target, MonoBehaviour targetObject, int damage)
        {
            Bat bat = Instantiate(_bat, BackgroundMover.Instance.transform);
            Vector3 offset = new Vector3(Random.Range(-_spawnBatsOffset.x, _spawnBatsOffset.x), Random.Range(-_spawnBatsOffset.y, _spawnBatsOffset.y), 0);
            bat.transform.position = _shotPoint.transform.position + offset;
            while (target != null && Vector3.Distance(bat.transform.position, target.HitPoint.position) > 0.1f)
            {
                yield return null;

                if (target.HitPoint == null || target.HitPoint?.transform == null)
                {
                    Destroy(bat.gameObject);
                    break;
                }
                else bat.Move(target.HitPoint);
            }

            if (target != null)
            {
                target.TakeDamage(damage);
                Destroy(bat.gameObject);
            }
        }

        #endregion
    }
}