using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Save;
using WOFL.Settings;
using Kamen.DataSave;
using Kamen.UI;

namespace WOFL.UI
{
    public class CardSlider : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _cardsAmountText;
        [SerializeField] private Slider _cardsAmountSlider;
        [Space]
        [SerializeField] private Image _buttonBackground;
        [SerializeField] private TextMeshProUGUI _buttonText;

        [Header("Settings")]
        [SerializeField] private string _betweenSign;
        [SerializeField] private string _levelUpText;

        [Header("Variables")]
        private UnitInfo _unitInfo;
        private UnitDataForSave _unitData;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _unitData.OnAmountCardsChanged -= UpdateCardAmountView;
        }

        #endregion

        #region Control Methods

        public void Initialize(UnitInfo unitInfo, UnitDataForSave unitData)
        {
            _unitInfo = unitInfo;
            _unitData = unitData;

            _unitData.OnAmountCardsChanged += UpdateCardAmountView;

            AdjustSlider(_unitInfo.LevelsHolder.Levels[_unitData.CurrentLevel].AmountCardToUpgrade);
            UpdateCardAmountView();
        }
        private void AdjustSlider(int maxValue)
        {
            _cardsAmountSlider.maxValue = maxValue;
            _cardsAmountSlider.value = 0;
        }
        private void UpdateCardAmountView()
        {
            _cardsAmountSlider.value = _unitData.AmountCards;
            int tolLevelUpValue = _unitInfo.LevelsHolder.Levels[_unitData.CurrentLevel].AmountCardToUpgrade;
            UpdateCardsAmountText(_unitData.AmountCards, tolLevelUpValue);
            AdjustButton(_unitData.AmountCards >= tolLevelUpValue);
        }
        private void UpdateCardsAmountText(int currentValue, int toLevelUpValue)
        {
            if (currentValue < toLevelUpValue) _cardsAmountText.text = $"{currentValue} {_betweenSign} {toLevelUpValue}";
            else _cardsAmountText.text = _levelUpText;
        }
        private void AdjustButton(bool isButtonActive)
        {
            _buttonBackground.gameObject.SetActive(isButtonActive);
            _buttonText.gameObject.SetActive(isButtonActive);
        }

        #endregion
    }
}