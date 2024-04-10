using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen
{
    [Serializable] public struct DateTimeConstructor
    {
        #region Variables

        [SerializeField] private int _year;
        [SerializeField] private int _month;
        [SerializeField] private int _day;

        #endregion

        #region Properties

        public int Year { get => _year; }
        public int Month { get => _month; }
        public int Day { get => _day; }

        #endregion
    }
}