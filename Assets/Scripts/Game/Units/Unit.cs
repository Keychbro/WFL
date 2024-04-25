using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;
using WOFL.Game.Components;
using WOFL.Settings;

namespace WOFL.Game
{
    public abstract class Unit : MonoBehaviour, IMoveable, IDamageable, IHealable, IDeathable
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Animator _unitAnimator;
        [SerializeField] private GameObject _unitSkin;

        [Header("Settings")]
        [SerializeField] private string _name;
        [SerializeField] private int _startHealthValue;

        [Header("Variables")]
        private IMoveable.MoveType _movingType;
        private int _currentHealth;
        private UnitInfo _unitInfo;
        private Vector3 _moveDirection;

        private IDamageable _currentTargetDamageable;
        private MonoBehaviour _currentTargetObject;

        public event Action<int> OnTakedDamage;
        public event Action<int> OnHealed;
        public event Action<IDeathable> OnDead;

        #endregion

        #region Properties

        public IDamageable.GameSideName SideName { get; private set; }
        public int MaxHealth { get => _startHealthValue; }
        public IMoveable.MoveType MovingType { get => _movingType; }

        #endregion

        #region Control Methods

        public void Initialize(UnitInfo unitInfo, IDamageable.GameSideName sideName)
        {
            SideName = sideName;
            _unitInfo = unitInfo;
            _currentHealth = _startHealthValue; // TODO: Fix this
            //_moveDirection = sideName == IDamageable.GameSideName.Enemy ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        }
       // private void 

        #endregion

        #region IMoveable Methods

        public void Move()
        {
            if (_currentTargetDamageable == null) return;

            if (_currentTargetObject.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                _moveDirection = new Vector3(-1, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _moveDirection = new Vector3(1, 0, 0);
            }

            transform.position += _moveDirection * _unitInfo.MoveSpeed * Time.fixedDeltaTime;
        }

        #endregion

        #region IDamagable Methods

        public virtual void TakeDamage(int value)
        {
            if (value < 0) return;

            _currentHealth -= value;
            OnTakedDamage?.Invoke(value);
        }

        #endregion

        #region IHealable Methods

        public virtual void Heal(int value)
        {
            if (value < 0) return;

            _currentHealth += value;
            if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
            OnHealed?.Invoke(value);
        }

        #endregion

        #region IDeathable Methods

        public virtual void Death()
        {
            OnDead.Invoke(this);
        }

        #endregion

        #region Calculate Methods

        public void FindClosestTarget(List<IDamageable> allTargets)
        {
            IDamageable closestTarget = allTargets[0];
            float minDistance = Vector3.Distance(transform.position, GetIDamageablePosition(closestTarget));

            for (int i = 1; i < allTargets.Count; i++)
            {
                float distance = Vector2.Distance(transform.position, GetIDamageablePosition(closestTarget));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = allTargets[i];
                }
            }

            _currentTargetDamageable = closestTarget;

            Type damageableType = _currentTargetDamageable.GetType();
            if (typeof(MonoBehaviour).IsAssignableFrom(damageableType))
            {
                _currentTargetObject = (MonoBehaviour)_currentTargetDamageable;
            }
        }

        private Vector3 GetIDamageablePosition(IDamageable iDamageable)
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
    }
}
