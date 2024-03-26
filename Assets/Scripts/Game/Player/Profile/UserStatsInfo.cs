using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WOFL.Stats
{
    public class UserStatsInfo : ScriptableObject
    {
        #region Variables

        [SerializeField] private Sprite _statsIconBackground;
        [SerializeField] private Sprite _statsIcon;
        [SerializeField] private string _statsName;
        [SerializeField] private int _startValue;

        #endregion

        #region Properties

        public string StatsName { get => _statsName; }
        public Sprite StatsIconBackground { get => _statsIconBackground; }
        public Sprite StatsIcon { get => _statsIcon; }
        public int StartValue { get => _startValue; }

        #endregion
    }
}