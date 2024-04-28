using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;
using WOFL.Game.Components;
using WOFL.Save;
using WOFL.Settings;

namespace WOFL.Game
{
    public abstract class Unit : MonoBehaviour, IDamageable, IHealable, IDeathable
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] protected Animator _unitAnimator;
        [SerializeField] protected GameObject _unitSkin;

        [Header("Variables")]
        
        protected int _currentHealth;

        protected UnitInfo _currentUnitInfo;
        protected int _levelNumber;

        public event Action<int> OnTakedDamage;
        public event Action<int> OnHealed;
        public bool IsAlive { get; private set; }
        public event Action<IDeathable> OnDead;

        #endregion

        #region Properties

        public IDamageable.GameSideName SideName { get; private set; }
        public int MaxHealth { get; private set; }

        #endregion

        #region Control Methods

        public virtual void Initialize(UnitInfo unitInfo, int levelNumber, IDamageable.GameSideName sideName)
        {
            SideName = sideName;
            _currentUnitInfo = unitInfo;
            _levelNumber = levelNumber;

            MaxHealth = _currentUnitInfo.LevelsHolder.Levels[_levelNumber].MaxHealthValue;
            _currentHealth = MaxHealth;
            IsAlive = true;
        }
        public abstract void ControlUnit();

        #endregion

        #region IDamagable Methods

        public virtual void TakeDamage(int value)
        {
            if (value < 0) return;

            _currentHealth -= value;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Death();
            }

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
            _unitAnimator.SetTrigger("Dead");
        }

        #endregion
    }
}
