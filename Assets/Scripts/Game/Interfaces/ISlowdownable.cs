using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    public interface ISlowdownable
    {
        #region Properties

        public float SpeedIncreaseValue { get; }
        public Coroutine SlowingCoroutine { get; }
        public event Action OnSlowed;

        #endregion

        #region Methods

        public void Slow(float increaseValue, float duration);
        public IEnumerator SlowingControl(float duration);

        #endregion
    }
}