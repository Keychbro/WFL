using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Kamen
{
    [Serializable] public class Timer
    {
        #region Enums

        public enum State
        {
            Created,
            Activated,
            Stopped,
            IsOver
        }
        public enum Mode
        {
            InGame,
            Always
        }

        #endregion

        #region Variables

        [Header("Main Variables")]
        [SerializeField] private KamenTime _time;
        [SerializeField] private State _state;
        [SerializeField] private Mode _mode;
        [SerializeField] private bool _isSavedToData;
        [SerializeField] private int _stepInSeconds;

        public event Action<State> OnStateChanged;
        public event Action<KamenTime> OnTimeChanged;
        public event Action OnTimeIsOver;

        #endregion

        #region Properties

        public State CurrentState { get => _state; }
        public bool IsSavedToData { get => _isSavedToData; }
        public int StepInSeconds { get => _stepInSeconds; }
        public Mode CurrentMode { get => _mode; }

        #endregion

        #region Constructors

        public Timer() : this(new KamenTime(0), Mode.InGame, false, 1) { }
        public Timer(KamenTime time) : this(time, Mode.InGame, false, 1) { }
        public Timer(KamenTime time, Mode mode) : this(time, mode, false, 1) { }
        public Timer(KamenTime time, Mode mode, bool isSavedToData) : this(time, mode, isSavedToData, 1) { }
        public Timer(KamenTime time, Mode mode, bool isSavedToData, int stepInSeconds)
        {
            _time = time;
            _state = State.Created;
            _mode = mode;
            _isSavedToData = isSavedToData;
            _stepInSeconds = stepInSeconds;

            OnStateChanged?.Invoke(_state);
        }

        #endregion

        #region Control Methods

        public void Activate()
        {
            _state = State.Activated;
            OnStateChanged?.Invoke(_state);
            OnTimeChanged?.Invoke(_time);
        }
        public void Stop()
        {
            _state = State.Stopped;
            OnStateChanged?.Invoke(_state);
        }
        public void Destroy()
        {
            _state = State.IsOver;
            OnStateChanged?.Invoke(_state);
            OnTimeIsOver?.Invoke();
        }
        public void UpdateTimer(int value)
        {
            _time.ChangeOnValue(value);
            OnTimeChanged?.Invoke(_time);

            CheckingEnd();
        }
        public void CheckingEnd()
        {
            if (_time.AllInSeconds > 0) return;
            Destroy();
        }

        #endregion
    }
}

