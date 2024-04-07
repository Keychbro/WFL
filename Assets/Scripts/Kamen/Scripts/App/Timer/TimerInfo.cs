using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Kamen
{
    [Serializable] public class TimerInfo
    {
        #region Variables

        [SerializeField] private string _id;
        [SerializeField] private Timer _timer;
        [SerializeField] private TimerViewer[] _viewers;
        [SerializeField] private Coroutine _coroutine;

        #endregion

        #region Properties

        public string ID { get => _id; }
        public Timer Timer { get => _timer; }
        public TimerViewer[] Viewers { get => _viewers; }
        public Coroutine Coroutine
        {
            get => _coroutine;
            set
            {
                if (value != null) _coroutine = value;
            }
        }

        #endregion

        #region Constructors

        public TimerInfo(Timer timer) : this("Default Time", timer, null) { }
        public TimerInfo(string id, Timer timer) : this(id, timer, null) { }
        public TimerInfo(string id, Timer timer, TimerViewer[] viewers)
        {
            _id = id;
            _timer = timer;
            _viewers = viewers;
            InitiliazeViewer();
        }

        #endregion

        #region Control Methods

        public void InitiliazeViewer()
        {
            for (int i = 0; i < _viewers.Length; i++)
            {
                _viewers[i].Initialize(_timer);
            }
        }
        public void UpdateViewers(TimerViewer[] newViewers) => _viewers = newViewers;

        #endregion
    }
}
