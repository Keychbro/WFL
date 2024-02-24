using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.UI 
{
    [Serializable] public class BarRange
    {
        #region Variables

        [SerializeField] private int _minValue;
        [SerializeField] private int _maxValue;

        #endregion

        #region Properties

        public int MinValue { get => _minValue; }
        public int MaxValue { get => _maxValue; }

        #endregion
    }
}