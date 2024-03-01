using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public class Castle : MonoBehaviour, IManaOwnable, IDamagable, IDeathable
    {
        #region Variables

        [Header("Variables")]
        private int _currentHealth;
        private int _currentMana;

        #endregion

        #region Properties

        public int CurrentGold { get; private set; }
        public int MaxHealth { get; }

        public int Mana { get => _currentMana; }

        public event Action<int> OnTakedDamage;
        public event Action OnManaValueChanged;
        public event Action<float> OnManaFilled;

        #endregion

        #region IManaOwnable Methods

        private void Start()
        {
            StartCoroutine(Collect());
        }
        public IEnumerator Collect()
        {
            while (true)
            {
                DOVirtual.Float(0f, 1f, 1, CallOnManaFiller);
                yield return new WaitForSeconds(1);
                _currentMana++;
                OnManaValueChanged?.Invoke();
            }
        }
        private void CallOnManaFiller(float value) => OnManaFilled?.Invoke(value);

        #endregion

        #region IDamagable Methods

        public void TakeDamage(int value)
        {
            if (value < 0) return;

            _currentHealth -= value;
            OnTakedDamage?.Invoke(value);

            if (_currentHealth < 0) Death();
        }

        #endregion

        #region IDeathable Methods

        public void Death()
        {
           
        }

        #endregion
    }
}

