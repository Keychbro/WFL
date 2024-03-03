using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public interface IManaOwnable
    {
        #region Properties

        public int CurrentMana { get; }
        public float ManaFillDuration { get; }
        public event Action OnManaValueChanged;
        public event Action<float> OnManaFilled;

        #endregion

        #region Control Methods

        public IEnumerator Collect();

        #endregion
    }
}