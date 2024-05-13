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
    public class AttackingUnit :  Unit, IAttacking, IMoveable
    {
        #region  Variables

        [Header("Objets")]
        [SerializeField] protected Weapon _weapon;

        protected IMoveable.MoveType _movingType;
        protected IDamageable _currentTargetDamageable;
        protected MonoBehaviour _currentTargetObject;

        #endregion

        #region Properties

        public IMoveable.MoveType MovingType { get => _movingType; }
        public bool IsAttacking { get; protected set; }
        public Vector3 MoveDirection { get; protected set; }

        #endregion

        #region Control Methods

        public override void Initialize(UnitInfo unitInfo, int levelNumber, IDamageable.GameSideName sideName)
        {
            base.Initialize(unitInfo, levelNumber, sideName);
            if (_currentUnitInfo.WeaponInfo != null)
            {
                _weapon.Initialize(_currentUnitInfo.WeaponInfo.Levels[_levelNumber < _currentUnitInfo.WeaponInfo.Levels.Length ? _levelNumber : ^1]);
            }

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
                    if (CalculateDistanceOnXAxis(transform.position, _currentTargetObject.transform.position) >= _currentUnitInfo.WeaponInfo.Levels[_levelNumber].AttackRange)
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

        public virtual async void Attack()
        {
            if (IsAttacking || !IsAlive) return;

            IsAttacking = true;
            _unitAnimator.SetInteger("AttackValue", Random.Range(1, 3));
            _unitAnimator.SetBool("IsOnAttackRange", true);
            _unitAnimator.speed = _currentUnitInfo.WeaponInfo.Levels[_levelNumber].AttackSpeed / 100f;

            if (_weapon != null)
            {
                bool result = await _weapon.DoAttack(_currentTargetDamageable);
            }

            IsAttacking = false;
            _unitAnimator.SetBool("IsOnAttackRange", false);
        }
        public virtual void FindClosestTarget(List<IDamageable> allTargets)
        {
            IDamageable closestTarget = null;
            float minDistance = float.MaxValue;

            for (int i = 0; i < allTargets.Count; i++)
            {
                MonoBehaviour monoBehaviour = GetTargetMonoBehaviour(allTargets[i]);
                if (monoBehaviour == null) continue;

                float distance = Vector2.Distance(transform.position, monoBehaviour.transform.position);
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

        public virtual MonoBehaviour GetTargetMonoBehaviour(IDamageable iDamageable)
        {
            Type damageableType = iDamageable.GetType();

            if (typeof(MonoBehaviour).IsAssignableFrom(damageableType))
            {
                MonoBehaviour monoBehaviour = (MonoBehaviour)iDamageable;
                return monoBehaviour;
            }
            return null;
        }
        public float CalculateDistanceOnXAxis(Vector3 position1, Vector3 position2)
        {
            return Mathf.Abs(position2.x - position1.x);
        }
                    
        #endregion

        #region IMoveable Methods


        public virtual void Move()
        {
            if (!IsAlive) return;

            _movingType = IMoveable.MoveType.Going;
            if (_currentTargetDamageable.HitPoint.transform.position.x < transform.position.x)
            {
                _unitSkin.transform.eulerAngles = new Vector3(0, 180, 0);
                MoveDirection = new Vector3(-1, 0, 0);
            }
            else
            {
                _unitSkin.transform.eulerAngles = new Vector3(0, 0, 0);
                MoveDirection = new Vector3(1, 0, 0);
            }

            transform.position += MoveDirection * _currentUnitInfo.LevelsHolder.Levels[_levelNumber].MoveSpeed * Time.fixedDeltaTime / 100f;
            _unitAnimator.SetBool("IsHaveTarget", true);
        }
        public virtual void Stand()
        {
            _unitAnimator.SetBool("IsHaveTarget", false);
            _movingType = IMoveable.MoveType.Standing; 
            
        }

        #endregion
    }
}