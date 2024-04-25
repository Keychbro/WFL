using System;
using System.Collections;
using UnityEngine;

namespace WOFL.Game
{
    public interface IDeathable
    {
        #region Properties

        public event Action<IDeathable> OnDead;

        #endregion

        #region Control Methods

        public void Death();

        #endregion
    }
}