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

        [Header("City")]
        [SerializeField] private int _cityLevel;
        public event Action OnCityLevelChanged;

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

        #endregion

        #region Currency Properties
        public int Gold
        {
            get => _gold;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Data] - Attempt to assign variable ''_gold'' minus value");
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
                    Debug.LogError($"[Dats] - Attempy to assign variable ''_diamonds'' minus value ");
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
                    Debug.LogError($"[Dats] - Attempy to assign variable ''_tools'' minus value ");
                    return;
                }

                _tools = value;
                OnToolsAmountChanged?.Invoke();
            }
        }

        #endregion

        #region City Properties

        public int CityLevel
        {
            get => _cityLevel;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Data] - Attempt to assign variable ''_cityLevel'' minus value");
                    return;
                }

                _cityLevel = value;
                OnCityLevelChanged?.Invoke();
            }
        }

        #endregion
    }
}