using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.DataSave;
using System;

namespace Kamen
{
    public class TimerManager : SingletonComponent<TimerManager>
    {
        #region Classes

        [Serializable] private class TimerEditInfo
        {
            #region TimerEditInfo Variables

            [SerializeField] private string _id;
            [SerializeField] private TimerViewer[] _viewers;

            #endregion

            #region TimerEditInfo Properties

            public string ID { get => _id; }
            public TimerViewer[] Viewers { get => _viewers; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private string _saveTimerDataName;
        [SerializeField] private TimerEditInfo[] _timersEditInfo;

        [Header("Variables")]
        [SerializeField] private TimerData _timerData;
        private bool _isFirst = true;
        private int _timerUpdateDelay = 1;
        private float _currentTimerDelay = 0;

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }
        private void Update()
        {
            _currentTimerDelay += UnityEngine.Time.deltaTime;
            if (_currentTimerDelay >= _timerUpdateDelay)
            {
                _currentTimerDelay = 0;

                if (!_isFirst && !CheckQuitTimeRight(_timerUpdateDelay + 1))
                {
                    StopAllCounters();
                    StartAllCounters();
                    ConpenseteTime();
                    SetQuitTime();
                    PlayerPrefs.SetString(_saveTimerDataName, JsonUtility.ToJson(_timerData));
                }
                else
                {
                    SetQuitTime();
                }
                _isFirst = false;
            }
        }
        private void OnApplicationPause(bool pauseStatus)
        {
            //if (pauseStatus)
            //{
            //    SetQuitTime();
            //    StopAllCounters();
            //    PlayerPrefs.SetString(_saveTimerDataName, JsonUtility.ToJson(_timerData));
            //}
            //else 
            //{
            //    StartAllCounters();
            //    ConpenseteTime();
            //}
        }
        private void OnApplicationQuit()
        {
            SetQuitTime();
        }
        private void OnApplicationFocus(bool focus)
        {
            // SetQuitTime();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _timerData = JsonUtility.FromJson<TimerData>(PlayerPrefs.GetString(_saveTimerDataName, JsonUtility.ToJson(new TimerData())));

            for (int i = 0; i < _timerData.TimersInfo.Count; i++)
            {
                TimerEditInfo timerEditInfo = GetTimerEditInfoByID(_timerData.TimersInfo[i].ID);

                if (!_timerData.TimersInfo[i].Timer.IsSavedToData)
                {
                    _timerData.TimersInfo.RemoveAt(i);
                    i--;
                }
                else
                {
                    _timerData.TimersInfo[i].UpdateViewers(timerEditInfo.Viewers);
                    _timerData.TimersInfo[i].InitiliazeViewer();
                    SubscribeOnTimer(_timerData.TimersInfo[i]);
                }
            }
            ConpenseteTime();
        }
        public void RecordNewTimer(string id, Timer newTimer)
        {
            if (CheckToAlreadyUsedByID(id)) return;
            TimerEditInfo timerEditInfo = GetTimerEditInfoByID(id);

            TimerInfo newTimerInfo = new TimerInfo(timerEditInfo.ID, newTimer, timerEditInfo.Viewers);
            _timerData.TimersInfo.Add(newTimerInfo);
            PlayerPrefs.SetString(_saveTimerDataName, JsonUtility.ToJson(_timerData));

            SubscribeOnTimer(newTimerInfo);
        }
        public void ActivateTimer(string id)
        {
            TimerInfo info = GetTimerInfoByID(id);
            info.Timer.Activate();
        }
        public void StopTimer(string id)
        {
            TimerInfo info = GetTimerInfoByID(id);
            info.Timer.Stop();
        }
        public void DestroyTimer(string id)
        {
            if (!CheckToAlreadyUsedByID(id)) return;
            TimerInfo info = GetTimerInfoByID(id);
            info.Timer.Destroy();
        }
        public Timer GetSubscribeOnTimeOver(string id) => GetTimerInfoByID(id).Timer;
        public TimerInfo GetTimerInfo(string id) => GetTimerInfoByID(id);

        #endregion

        #region Counter Methods

        private void StartAllCounters()
        {
            for (int i = 0; i < _timerData.TimersInfo.Count; i++)
            {
                TimerInfo info = _timerData.TimersInfo[i];
                if (info.Timer.CurrentState != Timer.State.Activated) continue;
                info.Coroutine = StartCoroutine(Counter(info));
            }
        }
        private void StopAllCounters()
        {
            for (int i = 0; i < _timerData.TimersInfo.Count; i++)
            {
                if (_timerData.TimersInfo[i].Timer.CurrentState != Timer.State.Activated || _timerData.TimersInfo[i].Coroutine == null) continue;
                StopCoroutine(_timerData.TimersInfo[i].Coroutine);
            }
        }
        private void ConpenseteTime()
        {
            int conpenseteSeconds = CalculateConpenseteTime();
            for (int i = 0; i < _timerData.TimersInfo.Count; i++)
            {
                TimerInfo info = _timerData.TimersInfo[i];
                if (info.Timer.CurrentState != Timer.State.Activated || info.Timer.CurrentMode != Timer.Mode.Always) continue;
                info.Timer.UpdateTimer(-conpenseteSeconds);
            }
            PlayerPrefs.SetString(_saveTimerDataName, JsonUtility.ToJson(_timerData));
        }
        private IEnumerator Counter(TimerInfo timerInfo)
        {
            while (true)
            {
                yield return new WaitForSeconds(timerInfo.Timer.StepInSeconds);
                timerInfo.Timer.UpdateTimer(-timerInfo.Timer.StepInSeconds);
            }
        }
        private void SetQuitTime()
        {
            _timerData.QuitTime = DateTime.Now;
            _timerData.QuitTimeInTicks = DateTime.Now.Ticks;
            PlayerPrefs.SetString(_saveTimerDataName, JsonUtility.ToJson(_timerData));
        }
        private bool CheckQuitTimeRight(int goalValue)
        {
            TimeSpan timeDifference = DateTime.Now - new DateTime(_timerData.QuitTimeInTicks);

            if (timeDifference.TotalSeconds > goalValue) return false;
            else return false;
        }

        #endregion

        #region Timers Subscribe Methods

        private void SubscribeOnTimer(TimerInfo timerInfo)
        {
            timerInfo.Timer.OnStateChanged += (state) => TimerHandle(timerInfo.ID, state);
        }
        private void UnsubscribeFromTimer(TimerInfo timerInfo)
        {
            timerInfo.Timer.OnStateChanged -= (state) => TimerHandle(timerInfo.ID, state);
        }
        private void TimerHandle(string timerID, Timer.State state)
        {
            TimerInfo info = GetTimerInfoByID(timerID);

            switch (state)
            {
                case Timer.State.Created:
                    break;
                case Timer.State.Activated:
                    info.Coroutine = StartCoroutine(Counter(info));
                    break;
                case Timer.State.Stopped:
                    StopCoroutine(info.Coroutine);
                    break;
                case Timer.State.IsOver:
                    if (info.Coroutine != null) StopCoroutine(info.Coroutine);
                    UnsubscribeFromTimer(info);
                    _timerData.TimersInfo.Remove(info);
                    break;
            }

            PlayerPrefs.SetString(_saveTimerDataName, JsonUtility.ToJson(_timerData));
        }

        #endregion

        #region Calculation Methods

        private TimerEditInfo GetTimerEditInfoByID(string id)
        {
            for (int i = 0; i < _timersEditInfo.Length; i++)
            {
                if (id == _timersEditInfo[i].ID) return _timersEditInfo[i];
            }

            Debug.LogError($"[TimerManager] - Time Edit Info with id {id} doesn't exist!");
            return null;
        }
        private TimerInfo GetTimerInfoByID(string id)
        {
            for (int i = 0; i < _timerData.TimersInfo.Count; i++)
            {
                if (id == _timerData.TimersInfo[i].ID) return _timerData.TimersInfo[i];
            }
            //Debug.LogError($"[TimerManager] - Time Info with id {id} doesn't exist!");
            return null;
        }
        private bool CheckToAlreadyUsedByID(string id)
        {
            for (int i = 0; i < _timerData.TimersInfo.Count; i++)
            {
                if (id == _timerData.TimersInfo[i].ID)
                {
                    Debug.Log($"[TimerManager] - Time with id {id} already used!");
                    return true;
                }
            }

            return false;
        }
        private int CalculateConpenseteTime()
        {
            TimeSpan timeDifference = DateTime.Now - new DateTime(_timerData.QuitTimeInTicks); // Вычисляем разницу времени
            return (int)Math.Round(timeDifference.TotalSeconds, 0); // Получаем разницу в секундах
        }

        #endregion
    }

    [Serializable]
    public class TimerData
    {
        #region Variables

        [SerializeField] private List<TimerInfo> _timersInfo = new List<TimerInfo>();
        [SerializeField] private DateTime _quitTime;

        #endregion

        #region Properties

        public List<TimerInfo> TimersInfo { get => _timersInfo; }
        public long QuitTimeInTicks = DateTime.Now.Ticks;
        public DateTime QuitTime
        {
            get => _quitTime;
            set
            {
                if (value != null) _quitTime = value;
            }
        }

        #endregion
    }
}