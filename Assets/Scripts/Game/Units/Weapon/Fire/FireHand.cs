using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Game;

namespace WOFL.Game
{
    public class FireHand : FireWeapon
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private Fireball _fireball;

        [Header("Settings")]
        [SerializeField] private GameObject _shotPoint;
        [SerializeField] private Vector3 _spawnFireballOffset;

        #endregion

        #region Control Methods

        public override async UniTask<bool> DoAttack(IDamageable target, MonoBehaviour targetObject)
        {
            float attackTime = 100f / _currentWeaponLevel.AttackSpeed;
            await UniTask.Delay(Mathf.RoundToInt(attackTime * _finishAttackPoint * 1000));
            if (!_isCanAttack) return false;

            StartCoroutine(Shot(target, targetObject, _currentWeaponLevel.Damage));
            await UniTask.Delay(Mathf.RoundToInt(attackTime * (1 - _finishAttackPoint) * 1000));
            return true;
        }
        private IEnumerator Shot(IDamageable target, MonoBehaviour targetObject, int damage)
        {
            Fireball fireball = Instantiate(_fireball, BackgroundMover.Instance.transform);
            Vector3 offset = new Vector3(Random.Range(-_spawnFireballOffset.x, _spawnFireballOffset.x), Random.Range(-_spawnFireballOffset.y, _spawnFireballOffset.y), 0);
            fireball.transform.position = _shotPoint.transform.position + offset;
            while (target != null && Vector3.Distance(fireball.transform.position, target.HitPoint.position) > 0.1f)
            {
                if (target.HitPoint == null)
                {
                    Destroy(fireball.gameObject);
                    break;
                }
                fireball.Move(target.HitPoint);
                yield return null;
            }

            if (target != null)
            {
                if (_currentFireWeaponLevel.BurnRadius == 0) target.TakeDamage(damage); 
                else GameManager.Instance.CallTakeDamageInPointWithRadius(target.SideName, targetObject.transform.position, _currentFireWeaponLevel.BurnRadius, damage);

                if (targetObject.TryGetComponent(out IFlammable flammable)) Flame(flammable);

                Destroy(fireball.gameObject);
            }
        }

        #endregion
    }
}