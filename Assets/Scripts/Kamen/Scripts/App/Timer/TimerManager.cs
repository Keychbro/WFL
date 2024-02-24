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
            [SerializeField] private TimerViewer _viewer;

            #endregion

            #region TimerEditInfo Properties

            public string ID { get => _id; }
            public TimerViewer Viewer { get => _viewer; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private TimerEditInfo[] _timersEditInfo;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SetQuitTime();
                StopAllCounters();
                DataSaveManager.Instance.SaveData();
            }
            else 
            {
                StartAllCounters();
                ConpenseteTime();
            }
        }
        private void OnApplicationQuit()
        {
            SetQuitTime();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            for (int i = 0; i < DataSaveManager.Instance.MyData.TimersInfo.Count; i++)
            {
                if (!DataSaveManager.Instance.MyData.TimersInfo[i].Timer.IsSavedToData)
                {
                    DataSaveManager.Instance.MyData.TimersInfo.RemoveAt(i);
                    i--;
                }
                else
                {
                    DataSaveManager.Instance.MyData.TimersInfo[i].InitiliazeViewer();
                    SubscribeOnTimer(DataSaveManager.Instance.MyData.TimersInfo[i]);
                }
            }
        }
        public void RecordNewTimer(string id, Timer newTimer)
        {
            if (CheckToAlreadyUsedByID(id)) return;
            TimerEditInfo timerEditInfo = GetTimerEditInfoByID(id);

            TimerInfo newTimerInfo = new TimerInfo(timerEditInfo.ID, newTimer, timerEditInfo.Viewer);
            DataSaveManager.Instance.MyData.TimersInfo.Add(newTimerInfo);
            DataSaveManager.Instance.SaveData();

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
            if(!CheckToAlreadyUsedByID(id)) return;
            TimerInfo info = GetTimerInfoByID(id);
            info.Timer.Destroy();
        }
        public Timer SubscribeOnTimeOver(string id) => GetTimerInfoByID(id).Timer;
        public Action SubscribeOnTimeOver2(string id) => GetTimerInfoByID(id).Timer.OnTimeIsOver;
        public Action UnsubscribeOnTimeOver(string id) => GetTimerInfoByID(id).Timer.OnTimeIsOver;

        #endregion

        #region Counter Methods

   
        private void StartAllCounters()
        {
            for (int i = 0; i < DataSaveManager.Instance.MyData.TimersInfo.Count; i++)
            {
                TimerInfo info = DataSaveManager.Instance.MyData.TimersInfo[i];
                if (info.Timer.CurrentState != Timer.State.Activated) continue;
                info.Coroutine = StartCoroutine(Counter(info));
            }
        }
        private void StopAllCounters()
        {
            for (int i = 0; i < DataSaveManager.Instance.MyData.TimersInfo.Count; i++)
            {
                if (DataSaveManager.Instance.MyData.TimersInfo[i].Timer.CurrentState != Timer.State.Activated) continue;   
                StopCoroutine(DataSaveManager.Instance.MyData.TimersInfo[i].Coroutine);
            }
        }
        private void ConpenseteTime()
        {
            int conpenseteSeconds = CalculateConpenseteTime();
            for (int i = 0; i < DataSaveManager.Instance.MyData.TimersInfo.Count; i++)
            {
                TimerInfo info = DataSaveManager.Instance.MyData.TimersInfo[i];
                if (info.Timer.CurrentState != Timer.State.Activated || info.Timer.CurrentMode != Timer.Mode.Always) continue;
                info.Timer.UpdateTimer(-conpenseteSeconds);
            }
            DataSaveManager.Instance.SaveData();
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
            DataSaveManager.Instance.MyData.QuitTime = DateTime.Now;
            DataSaveManager.Instance.SaveData();
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

            switch(state)
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
                    StopCoroutine(info.Coroutine);
                    UnsubscribeFromTimer(info);
                    DataSaveManager.Instance.MyData.TimersInfo.Remove(info);
                    break;
            }

            DataSaveManager.Instance.SaveData();
        }   

        #endregion

        #region Calculation Methods

        private TimerEditInfo GetTimerEditInfoByID(string id)
        {
            for (int i = 0; i < _timersEditInfo.Length; i++)
            {
                if (id == _timersEditInfo[i].ID) return _timersEditInfo[i];
            }
            
            Debug.LogError($"[TimerManager] - Timer Edit Info with id {id} doesn't exist!");
            return null;
        }
        private TimerInfo GetTimerInfoByID(string id)
        {
            for (int i = 0; i < DataSaveManager.Instance.MyData.TimersInfo.Count; i++)
            {
                if (id == DataSaveManager.Instance.MyData.TimersInfo[i].ID) return DataSaveManager.Instance.MyData.TimersInfo[i];
            }
            Debug.LogError($"[TimerManager] - Timer Info with id {id} doesn't exist!");
            return null;
        }
        private bool CheckToAlreadyUsedByID(string id)
        {
            for (int i = 0; i < DataSaveManager.Instance.MyData.TimersInfo.Count; i++)
            {
                if (id == DataSaveManager.Instance.MyData.TimersInfo[i].ID) 
                {
                    Debug.Log($"[TimerManager] - Timer with id {id} already used!");
                    return true;
                }
            }

            return false;
        }
        private int CalculateConpenseteTime()
        {
            TimeSpan timeDifference = DateTime.Now - DataSaveManager.Instance.MyData.QuitTime; // Вычисляем разницу времени
            return (int)Math.Round(timeDifference.TotalSeconds, 0); // Получаем разницу в секундах
        }

        #endregion
    }   
}