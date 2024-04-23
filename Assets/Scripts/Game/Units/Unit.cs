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
        public event Action<int> OnTakedDamage;
        public event Action<int> OnHealed;

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
            _moveDirection = sideName == IDamageable.GameSideName.Enemy ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        }

        #endregion

        #region IMoveable Methods

        public void Move()
        {
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

        }

        #endregion
    }
}
