using System;
using System.Collections;
using UnityEngine;

namespace WOFL.Game
{
    public interface IDeathable
    {
        #region Properties

        public bool IsAlive { get; }
        public event Action<IDeathable> OnDead;

        #endregion

        #region Control Methods

        public void Death();

        #endregion
    }
}