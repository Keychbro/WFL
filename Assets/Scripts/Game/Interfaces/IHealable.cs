using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public interface IHealable
    {
        #region Variables

        public event Action<int> OnHealed;

        #endregion

        #region Control Methods

        public void Heal(int value);

        #endregion
    }
}