using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Kamen 
{
    [Serializable] public struct KamenTime
    {
        #region KamenTime Variables

        [SerializeField] private int _hours;
        [SerializeField] private int _minutes;
        [SerializeField] private int _seconds;

        #endregion

        #region KamenTime Propeties

        public int Hours { get => _hours; }
        public int Minutes { get => _minutes; }
        public int Seconds { get => _seconds; }
        public int AllInSeconds 
        { 
            get 
            {
                return (_hours * 60 + _minutes) * 60 + _seconds;
            }
        }

        #endregion

        #region KamenTime Constructors

        public KamenTime(DateTime dateTime) : this(dateTime.Hour, dateTime.Minute, dateTime.Second) { }
        public KamenTime(int seconds) : this(0, 0, seconds) { }
        public KamenTime(int minutes, int seconds) : this(0, minutes, seconds) { }
        public KamenTime(int hours, int minutes, int seconds)
        {
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;

            CheckTime();
        }

        #endregion

        #region Control Methods
        public void CheckTime()
        {
            if (_seconds > 60) CheckValueForMax(60, ref _seconds, ref _minutes);
            else if (_seconds < 0) CheckValueForMin(60, ref _seconds, ref _minutes);

            if (_minutes > 60) CheckValueForMax(60, ref _minutes, ref _hours);
            else if (_minutes < 0) CheckValueForMin(60, ref _minutes, ref _hours);

            if (AllInSeconds <= 0)
            {
                _hours = 0;
                _minutes = 0;
                _seconds = 0;
            }
        }
        private void CheckValueForMin(int maxValue, ref int value, ref int nextValue)
        {
            int extraValue = value / maxValue;
            value += maxValue;
            nextValue -= extraValue + 1;
        }
        private void CheckValueForMax(int maxValue, ref int value, ref int nextValue)
        {
            int extraValue = value / maxValue;
            value -= extraValue * maxValue;
            nextValue += extraValue;           
        }

        #endregion

        #region Update Time Methods

        public void ChangeOnValue(int offsetSeconds) => ChangeOnValue(0, 0, offsetSeconds);
        public void ChangeOnValue(int offsetMinutes, int offsetSeconds) => ChangeOnValue(0, offsetMinutes, offsetSeconds);
        public void ChangeOnValue(int offsetHours, int offsetMinutes, int offsetSeconds)
        {
            _hours += offsetHours;
            _minutes += offsetMinutes;
            _seconds += offsetSeconds;

            CheckTime();
        }

        #endregion
    }
}