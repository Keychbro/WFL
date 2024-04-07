using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen
{
    public static class KamenTimeUnits
    {
        #region Enums

        public enum Unit
        {
            Days,
            Hours,
            Minutes,
            Seconds,
        }

        #endregion

        #region Variables

        private static Dictionary<Unit, int> _maxUnitsValue = new Dictionary<Unit, int>()
        {
            { Unit.Days, -1 },
            { Unit.Hours, 24 },
            { Unit.Minutes, 60 },
            { Unit.Seconds, 60 },
        };
        private static Dictionary<Unit, int> _minUnitsValue = new Dictionary<Unit, int>()
        {
            { Unit.Days, 0 },
            { Unit.Hours, 0 },
            { Unit.Minutes, 0 },
            { Unit.Seconds, 0 },
        };

        #endregion

        #region Control Methods

        public static int GetMaxUnitValue(Unit unit) => _maxUnitsValue[unit];
        public static int GetMinUnitValue(Unit unit) => _minUnitsValue[unit];

        #endregion
    }
}