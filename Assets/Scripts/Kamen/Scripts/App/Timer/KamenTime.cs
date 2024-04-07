using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Kamen 
{
    [Serializable] public struct KamenTime
    {
        #region Enums

        private enum Unit
        {
            Days,
            Hours,
            Minutes,
            Seconds
        }

        #endregion

        #region Variables

        [SerializeField] private int _days;
        [SerializeField] private int _hours;
        [SerializeField] private int _minutes;
        [SerializeField] private int _seconds;

        // private Dictionary<Unit, int> _unitMaxValue = new Dictionary<Unit, int>();

        #endregion

        #region Propeties

        public int Days { get => _days; }
        public int Hours { get => _hours; }
        public int Minutes { get => _minutes; }
        public int Seconds { get => _seconds; }
        public int AllInSeconds
        {
            get
            {
                return ((_days * KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Hours) + _hours) * KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Minutes) + _minutes) * KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Seconds) + _seconds;
            }
        }

        #endregion

        #region Constructors

        public KamenTime(DateTime dateTime) : this(dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second) { }
        public KamenTime(int seconds) : this(0, 0, 0, seconds) { }
        public KamenTime(int minutes, int seconds) : this(0, 0, minutes, seconds) { }
        public KamenTime(int hours, int minutes, int seconds) : this(0, hours, minutes, seconds) { }
        public KamenTime(int days, int hours, int minutes, int seconds)
        {
            _days = days;
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;

            CheckTime();
        }

        #endregion

        #region Control Methods
        public void CheckTime()
        {
            if (_seconds > KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Seconds)) CheckValueForMax(KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Seconds), ref _seconds, ref _minutes);
            else if (_seconds < KamenTimeUnits.GetMinUnitValue(KamenTimeUnits.Unit.Seconds)) CheckValueForMin(KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Seconds), ref _seconds, ref _minutes);

            if (_minutes > KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Minutes)) CheckValueForMax(KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Minutes), ref _minutes, ref _hours);
            else if (_minutes < KamenTimeUnits.GetMinUnitValue(KamenTimeUnits.Unit.Minutes)) CheckValueForMin(KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Minutes), ref _minutes, ref _hours);

            if (_hours > KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Hours)) CheckValueForMax(KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Hours), ref _hours, ref _days);
            else if (_hours < KamenTimeUnits.GetMinUnitValue(KamenTimeUnits.Unit.Hours)) CheckValueForMin(KamenTimeUnits.GetMaxUnitValue(KamenTimeUnits.Unit.Hours), ref _hours, ref _days);

            if (AllInSeconds <= 0)
            {
                _days = 0;
                _hours = 0;
                _minutes = 0;
                _seconds = 0;
            }
        }
        private void CheckValueForMin(int maxValue, ref int value, ref int nextValue)
        {
            int extraValue = value / maxValue - 1;
            value -= extraValue * maxValue;
            nextValue += extraValue;
        }
        private void CheckValueForMax(int maxValue, ref int value, ref int nextValue)
        {
            int extraValue = value / maxValue;
            value -= extraValue * maxValue;
            nextValue += extraValue;
        }

        #endregion

        #region Update Time Methods

        public void ChangeOnValue(int offsetSeconds) => ChangeOnValue(0, 0, 0, offsetSeconds);
        public void ChangeOnValue(int offsetMinutes, int offsetSeconds) => ChangeOnValue(0, 0, offsetMinutes, offsetSeconds);
        public void ChangeOnValue(int offsetHours, int offsetMinutes, int offsetSeconds) => ChangeOnValue(0, offsetHours, offsetMinutes, offsetSeconds);
        public void ChangeOnValue(int offsetDays, int offsetHours, int offsetMinutes, int offsetSeconds)
        {
            _days += offsetDays;
            _hours += offsetHours;
            _minutes += offsetMinutes;
            _seconds += offsetSeconds;

            CheckTime();
        }

        #endregion
    }
}