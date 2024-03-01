using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public interface IManaOwnable
    {
        #region Properties

        public int Mana { get; }
        public event Action OnManaValueChanged;
        public event Action<float> OnManaFilled;

        #endregion

        #region Control Methods

        public IEnumerator Collect();

        #endregion
    }
}