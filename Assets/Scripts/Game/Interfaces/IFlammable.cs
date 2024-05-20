using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public interface IFlammable
    {
        #region Properties

        public Coroutine BurningCoroutine { get; }
        public event Action OnBurned;

        #endregion

        #region Methods

        public void Burn(int damage, float duration);
        public IEnumerator Burning(int damage, float duration);

        #endregion
    }
}