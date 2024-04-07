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
            DD_HH_MM_SS_Full,
            DD_HH,
            HH_MM_SS_Full,
            HH_MM_SS_Min,
            MM_SS_Full,
            SS_MM_HH_Full,
            SS_MM_HH_Min,
            JustTwoUnits_Full,
        }

        #endregion

        #region Control Methods

        static public string ConvertKamenTimeToString(KamenTime time, TimeFormatInfo formatInfo, TimeOrder order)
        {
            string days = (time.Days < 10 ? "0" : "") + time.Days + formatInfo.DaysExtraText;
            string hours = (time.Hours < 10 ? "0" : "") + time.Hours + formatInfo.HoursExtraText;
            string minutes = (time.Minutes < 10 ? "0" : "") + time.Minutes + formatInfo.MinutesExtraText;
            string seconds = (time.Seconds < 10 ? "0" : "") + time.Seconds + formatInfo.SecondsExtraText;

            return order switch
            {
                TimeOrder.DD_HH_MM_SS_Full => days + formatInfo.SeparatorMark + hours + formatInfo.SeparatorMark + minutes + formatInfo.SeparatorMark + seconds,
                TimeOrder.DD_HH => days + formatInfo.SeparatorMark + hours + formatInfo.SeparatorMark,
                TimeOrder.HH_MM_SS_Full => hours + formatInfo.SeparatorMark + minutes + formatInfo.SeparatorMark + seconds,
                TimeOrder.HH_MM_SS_Min => ((time.Hours == 0) ? "" : hours + formatInfo.SeparatorMark) + ((time.Hours == 0 && time.Minutes == 0) ? "" : minutes + formatInfo.SeparatorMark) + seconds,
                TimeOrder.MM_SS_Full => minutes + formatInfo.SeparatorMark + seconds,
                TimeOrder.SS_MM_HH_Full => seconds + formatInfo.SeparatorMark + minutes + formatInfo.SeparatorMark + hours,
                TimeOrder.SS_MM_HH_Min => seconds + ((time.Hours == 0 && time.Minutes == 0) ? "" : formatInfo.SeparatorMark + minutes) + ((time.Hours == 0) ? "" : formatInfo.SeparatorMark + hours),
                TimeOrder.JustTwoUnits_Full => time.Days > 0 ? days + formatInfo.SeparatorMark + hours :
                                               time.Hours > 0 ? hours + formatInfo.SeparatorMark + minutes :
                                               minutes + formatInfo.SeparatorMark + seconds,
                _ => ""

            };

            //if (remainingTime.TotalHours <= 24) timerTmp.text = remainingTime.Hours + "h" + remainingTime.Minutes + "m";
            //else if (remainingTime.Hours < 1) timerTmp.text = remainingTime.Minutes + "m" + remainingTime.Seconds + "s";
            //else timerTmp.text = remainingTime.Days + "d" + remainingTime.Hours + "h";
        }

        #endregion
    }
}