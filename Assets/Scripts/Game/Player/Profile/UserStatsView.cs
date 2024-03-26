using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Stats;
using WOFL.DataSave;

namespace WOFL.UI
{
    public class UserStatsView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _iconBackground;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _statsName;
        [SerializeField] private TextMeshProUGUI _statsValue;

        [Header("Variables")]
        private UserStatsInfo _info;
        private UserStatsData _data;

        #endregion

        #region Control Methods

        public void Initialize(UserStatsInfo info, UserStatsData data)
        {
            _info = info;
            _data = data;

            _data.OnValueChanged += UpdateValue;

            _iconBackground.sprite = _info.StatsIconBackground;
            _icon.sprite = _info.StatsIcon;
            _statsName.text = _info.StatsName;
            UpdateValue();
        }
        private void UpdateValue()
        {
            _statsValue.text = _data.Value.ToString();
        }

        #endregion
    }
}