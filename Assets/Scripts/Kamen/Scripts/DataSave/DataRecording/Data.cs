using System;
using UnityEngine;
using System.Collections.Generic;

namespace Kamen.DataSave
{
    [Serializable] public class Data
    {
        #region Variables

        [SerializeField] private List<TimerInfo> _timersInfo = new List<TimerInfo>();
        [SerializeField] private DateTime _quitTime;

        [Header("Currency")]
        [SerializeField] private int _gold;
        public event Action OnGoldAmountChanged;
        [SerializeField] private int _diamonds;
        public event Action OnDiamondsAmountChanged;
        [SerializeField] private int _tools;
        public event Action OnToolsAmountChanged;

        public Action OnDataChanged;

        #endregion

        #region Properties

        public List<TimerInfo> TimersInfo { get => _timersInfo; }
        public DateTime QuitTime 
        { 
            get => _quitTime;
            set 
            {
                if (value != null) _quitTime = value;
            }
        }

        public int Gold
        {
            get => _gold;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Data] - Attempt to assign variable ''_gold'' 0 value");
                    return;
                }

                _gold = value;
                OnGoldAmountChanged?.Invoke();
            }
        }
        public int Diamonds
        {
            get => _diamonds;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Dats] - Attempy to assign variable ''_diamonds'' 0 value ");
                    return;
                }

                _diamonds = value;
                OnDiamondsAmountChanged?.Invoke();
            }
        }
        public int Tools
        {
            get => _tools;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Dats] - Attempy to assign variable ''_tools'' 0 value ");
                    return;
                }

                _tools = value;
                OnToolsAmountChanged?.Invoke();
            }
        }

        #endregion
    }
}