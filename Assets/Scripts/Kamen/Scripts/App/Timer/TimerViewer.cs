using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Kamen
{
    public class TimerViewer : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] protected TextMeshProUGUI _timerText;
        protected Timer _timer;

        [Header("Settings")]
        [SerializeField] protected TimeFormatter.TimeOrder _timeOrder;
        [SerializeField] protected TimeFormatInfo _timeFormatInfo;

        #endregion

        #region Control Methods

        public virtual void Initialize(Timer timer)
        {
            _timer = timer;
            _timer.OnTimeChanged += UpdateTimerText;
            _timer.OnTimeIsOver += UnsubscribeFromAction;
        }
        public virtual void UnsubscribeFromAction()
        {
            _timer.OnTimeChanged -= UpdateTimerText;
            _timer.OnTimeIsOver -= UnsubscribeFromAction;
        }
        protected virtual void UpdateTimerText(KamenTime time) => _timerText.SetText(TimeFormatter.ConvertKamenTimeToString(time, _timeFormatInfo, _timeOrder));

        #endregion
    }
}