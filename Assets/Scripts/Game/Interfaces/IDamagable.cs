using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public interface IDamagable
    {
        #region Properties

        public int MaxHealth { get; }
        public event Action<int> OnTakedDamage;

        #endregion

        #region Methods

        public void TakeDamage(int value);

        #endregion
    }
}