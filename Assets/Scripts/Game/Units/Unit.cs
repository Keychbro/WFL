using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;
using WOFL.Game.Components;

namespace WOFL.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Unit : MonoBehaviour, IMoveable, IDamagable, IHealable, IDeathable
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _name;
        [SerializeField] private int _startHealthValue;
        [SerializeField] private Sprite _appearance;

        [Header("Variables")]
        private IMoveable.MoveType _movingType;
        private int _currentHealth;
        public event Action<int> OnTakedDamage;
        public event Action<int> OnHealed;

        #endregion

        #region Properties

        public int MaxHealth { get => _startHealthValue; }
        public IMoveable.MoveType MovingType { get => _movingType; }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _currentHealth = _startHealthValue;
        }

        #endregion

        #region IMoveable Methods

        public void Move()
        {
            
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
