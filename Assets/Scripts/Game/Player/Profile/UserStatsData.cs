using UnityEngine;
using System;

namespace WOFL.DataSave
{
    [Serializable]
    public class UserStatsData
    {
        #region Variables

        [Header("Save Datas Variables")]
        [SerializeField] private string _statsName;
        [SerializeField] private int _value;

        public event Action OnValueChanged;

        #endregion

        #region Properties

        public string StatsName { get => _statsName; }
        public int Value
        {
            get => _value;
            set
            {
                if (value < 0)
                {
                    Debug.LogError("[UsetStatsData] - Trying to assign a value less than 0");
                    return;
                }

                _value = value;
            }
        }

        #endregion

        #region Constructors

        public UserStatsData(string statsName, int startValue)
        {
            _statsName = statsName;
            _value = startValue;
        }

        #endregion
    }
}