using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public interface IDamageable
    {
        #region Enums

        public enum GameSideName
        {
            None,
            Allied,
            Enemy
        }

        #endregion

        #region Properties

        public GameSideName SideName { get; }
        public int MaxHealth { get; }
        public event Action<int> OnTakedDamage;

        #endregion

        #region Methods

        public void TakeDamage(int value);

        #endregion
    }
}