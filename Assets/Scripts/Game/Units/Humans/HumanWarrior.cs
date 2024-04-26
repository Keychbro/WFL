using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Save;
using WOFL.Settings;
using Random = UnityEngine.Random;

namespace WOFL.Game
{
    public class HumanWarrior : Unit, IAttacking, IMoveable
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Weapon _weapon;

        private IMoveable.MoveType _movingType;
        private IDamageable _currentTargetDamageable;
        private MonoBehaviour _currentTargetObject;
        
        #endregion

        #region Properties

        public IMoveable.MoveType MovingType { get => _movingType; }
        public bool IsAttacking { get; private set; }
        public Vector3 MoveDirection { get; private set; }

        #endregion

        #region Control Methods

        public override void Initialize(UnitLevelInfo unitLevel, int levelNumber, IDamageable.GameSideName sideName)
        {
            base.Initialize(unitLevel, levelNumber, sideName);
            _weapon.Initialize(_currentLevel.WeaponInfo);

        }
        public override void ControlUnit()
        {
            if (!IsAlive) return;

            if (!GameManager.Instance.IsBattleStarted)
            {
                _unitAnimator.SetTrigger("Win");
            }
            else
            {
                if (_currentTargetObject == null)
                {
                    Stand();
                }
                else
                {
                    if (Vector3.Distance(transform.position, _currentTargetObject.transform.position) >= _currentLevel.WeaponInfo.AttackRange)
                    {
                        Move();
                    }
                    else
                    {
                        Attack();
                    }
                }
            }        
        }

        #endregion

        #region IAttacking Methods

        public async void Attack()
        {
            if (IsAttacking) return;

            IsAttacking = true;
            _unitAnimator.SetInteger("AttackValue", Random.Range(1, 3));
            _unitAnimator.SetBool("IsOnAttackRange", true);
            _unitAnimator.speed = 100f / _currentLevel.WeaponInfo.AttackSpeed;

            bool result = await _weapon.DoAttack(_currentTargetDamageable);

            IsAttacking = false;
        }
        public void FindClosestTarget(List<IDamageable> allTargets)
        {
            IDamageable closestTarget = null;
            float minDistance = float.MaxValue;

            for (int i = 0; i < allTargets.Count; i++)
            {
                float distance = Vector2.Distance(transform.position, GetTargetPosition(allTargets[i]));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = allTargets[i];
                }
            }

            _currentTargetDamageable = closestTarget;

            if (_currentTargetDamageable != null)
            {
                Type damageableType = _currentTargetDamageable.GetType();
                if (typeof(MonoBehaviour).IsAssignableFrom(damageableType))
                {
                    _currentTargetObject = (MonoBehaviour)_currentTargetDamageable;
                }
            }
            else _currentTargetObject = null;
        }

        public Vector3 GetTargetPosition(IDamageable iDamageable)
        {
            Type damageableType = iDamageable.GetType();

            if (typeof(MonoBehaviour).IsAssignableFrom(damageableType))
            {
                MonoBehaviour monoBehaviour = (MonoBehaviour)iDamageable;
                return monoBehaviour.transform.position;
            }
            return Vector3.zero;
        }

        #endregion

        #region IMoveable Methods


        public void Move()
        {
            _movingType = IMoveable.MoveType.Going;
            if (_currentTargetObject.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                MoveDirection = new Vector3(-1, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                MoveDirection = new Vector3(1, 0, 0);
            }

            transform.position += MoveDirection * _currentLevel.MoveSpeed * Time.fixedDeltaTime / 100f;
            _unitAnimator.SetBool("IsHaveTarget", true);
        }
        public void Stand()
        {
            _unitAnimator.SetBool("IsHaveTarget", false);
            _movingType = IMoveable.MoveType.Standing; 
            
        }

        #endregion
    }
}