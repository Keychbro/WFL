using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Control;

namespace WOFL.BattlePass
{
    public class MiniBattlePassView : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private Image _lineForSlider;

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Slider _slider;
        [SerializeField] private Transform _lineHolder;

        [Header("Variables")]
        private BattlePassManager.SeasonInfo _currentSeasonInfo;
        private BattlePassDataSave _currentBattlePassDataSave;
        private List<Image> _linesList = new List<Image>();

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
             _currentBattlePassDataSave.OnScoreChanged -= UpdateAmountCompletePoints;
        }

        #endregion

        #region Control Methods

        public void Initialize(int seasonNumber, BattlePassManager.SeasonInfo seasonInfo, BattlePassDataSave battlePassDataSave)
        {
            _currentSeasonInfo = seasonInfo;
            _currentBattlePassDataSave = battlePassDataSave;

            _titleText.text = $"Season {seasonNumber}";

            _amountText.text = $"{battlePassDataSave.Score/seasonInfo.BattlePassLine.ScoreForOneLevel}/{battlePassDataSave.TotalLevels}";
            _slider.maxValue = battlePassDataSave.TotalLevels;
            _slider.value = battlePassDataSave.Score/seasonInfo.BattlePassLine.ScoreForOneLevel;

            for (int i = 0; i < battlePassDataSave.TotalLevels; i++)
            {
                Image newLine = Instantiate(_lineForSlider, _lineHolder);
                _linesList.Add(newLine);
            }

            battlePassDataSave.OnScoreChanged += UpdateAmountCompletePoints;
        }

        private void UpdateAmountCompletePoints()
        {
            _amountText.text = $"{_currentBattlePassDataSave.Score/_currentSeasonInfo.BattlePassLine.ScoreForOneLevel}/{_currentBattlePassDataSave.TotalLevels}";
        }

        #endregion
    }
}