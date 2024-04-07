using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Kamen
{
    [Serializable] public struct TimeFormatInfo
    {
        #region Variables

        [SerializeField] private string _daysExtraText;
        [SerializeField] private string _hoursExtraText;
        [SerializeField] private string _minutesExtraText;
        [SerializeField] private string _secondsExtraText;
        [SerializeField] private string _separatorMark;

        #endregion

        #region Properties

        public string DaysExtraText { get => _daysExtraText; }
        public string HoursExtraText { get => _hoursExtraText; }
        public string MinutesExtraText { get => _minutesExtraText; }
        public string SecondsExtraText { get => _secondsExtraText; }
        public string SeparatorMark { get => _separatorMark; }

        #endregion

        #region Constructor

        public TimeFormatInfo(string separatorMark) : this("", "", "", "", separatorMark) { }
        public TimeFormatInfo(string daysExtraText, string hoursExtraText, string minutesExtraText, string secondsExtraText, string separatorMark)
        {
            _daysExtraText = daysExtraText;
            _hoursExtraText = hoursExtraText;
            _minutesExtraText = minutesExtraText;
            _secondsExtraText = secondsExtraText;
            _separatorMark = separatorMark;
        }

        #endregion
    }
}