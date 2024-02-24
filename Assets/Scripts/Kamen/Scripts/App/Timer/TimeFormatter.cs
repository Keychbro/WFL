using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen
{
    static public class TimeFormatter
    {
        #region Enums

        public enum TimeOrder
        {
            HH_MM_SS_Full,
            HH_MM_SS_Min,
            MM_SS_Full,
            SS_MM_HH_Full,
            SS_MM_HH_Min
        }

        #endregion

        #region Control Methods

        static public string ConvertKamenTimeToString(KamenTime time, TimeFormatInfo formatInfo, TimeOrder order)
        {
            string hours = (time.Hours < 10 ? "0" : "") + time.Hours + formatInfo.HoursExtraText;
            string minutes = (time.Minutes < 10 ? "0" : "") + time.Minutes + formatInfo.MinutesExtraText;
            string seconds = (time.Seconds < 10 ? "0" : "") + time.Seconds + formatInfo.SecondsExtraText;

            return order switch
            {
                TimeOrder.HH_MM_SS_Full => hours + formatInfo.SeparatorMark + minutes + formatInfo.SeparatorMark + seconds,
                TimeOrder.HH_MM_SS_Min => ((time.Hours == 0) ? "" : hours + formatInfo.SeparatorMark) + ((time.Hours == 0 && time.Minutes == 0) ? "" : minutes + formatInfo.SeparatorMark) + seconds,
                TimeOrder.MM_SS_Full => minutes + formatInfo.SeparatorMark + seconds,
                TimeOrder.SS_MM_HH_Full => seconds + formatInfo.SeparatorMark + minutes + formatInfo.SeparatorMark + hours,
                TimeOrder.SS_MM_HH_Min => seconds + ((time.Hours == 0 && time.Minutes == 0) ? "" : formatInfo.SeparatorMark + minutes) + ((time.Hours == 0) ? "" : formatInfo.SeparatorMark + hours),
                _ => ""
            };
        }

        #endregion
    }
}