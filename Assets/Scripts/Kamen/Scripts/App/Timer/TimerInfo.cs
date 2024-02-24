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
        [SerializeField] private TimerViewer _viewer;
        [SerializeField] private Coroutine _coroutine;

        #endregion

        #region Properties

        public string ID { get => _id; }
        public Timer Timer { get => _timer; }
        public TimerViewer Viewer { get => _viewer; }
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

        public TimerInfo(Timer timer) : this("Default Timer", timer, null) {}
        public TimerInfo(string id, Timer timer) : this(id, timer, null) {}
        public TimerInfo(string id, Timer timer, TimerViewer viewer)
        {
            _id = id;
            _timer = timer;
            _viewer = viewer;
            InitiliazeViewer();
        }

        #endregion

        #region Control Methods

        public void InitiliazeViewer() => _viewer.Initialize(_timer);

        #endregion
    }
}
