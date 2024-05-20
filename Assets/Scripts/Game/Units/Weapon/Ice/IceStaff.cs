using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Game;

namespace WOFL.Game
{
    public class IceStaff : IceWeapon
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private IceCrystal _iceCrystal;

        [Header("Settings")]
        [SerializeField] private int _amountCrystals;
        [SerializeField] private GameObject _shotPoint;
        [SerializeField] private Vector3 _spawnCrystalsOffset;

        #endregion

        #region Control Methods

        public override async UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            float attackTime = 100f / _currentWeaponLevel.AttackSpeed;
            await UniTask.Delay(Mathf.RoundToInt(attackTime * _finishAttackPoint * 1000));
            if (!_isCanAttack) return false;

            for (int i = 0; i < _amountCrystals; i++)
            {
                StartCoroutine(Shot(target, targetObject, _currentWeaponLevel.Damage / _amountCrystals));
            }
;
            await UniTask.Delay(Mathf.RoundToInt(attackTime * (1 - _finishAttackPoint) * 1000));
            return true;
        }
        private IEnumerator Shot(IDamageable target, MonoBehaviour targetObject, int damage)
        {
            IceCrystal iceCrystal = Instantiate(_iceCrystal, BackgroundMover.Instance.transform);
            Vector3 offset = new Vector3(Random.Range(-_spawnCrystalsOffset.x, _spawnCrystalsOffset.x), Random.Range(-_spawnCrystalsOffset.y, _spawnCrystalsOffset.y),0);
            iceCrystal.transform.position = _shotPoint.transform.position + offset;
            while (target != null && Vector3.Distance(iceCrystal.transform.position, target.HitPoint.position) > 0.1f)
            {
                if (target.HitPoint == null)
                {
                    Destroy(iceCrystal.gameObject);
                    break;
                }
                iceCrystal.Move(target.HitPoint);
                yield return null;
            }

            if (target != null)
            {
                target.TakeDamage(damage);
                if (targetObject.TryGetComponent(out ISlowdownable slow)) Slowdown(slow);
                Destroy(iceCrystal.gameObject);
            }
        }

        #endregion
    }
}