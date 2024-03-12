using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Save;
using WOFL.Settings;
using Kamen.DataSave;
using Kamen.UI;
using System.Linq;
using WOFL.Control;
using System;

namespace WOFL.UI
{
    public class UnitCardForUpgrade : MonoBehaviour
    {
        #region Variables

        [Header("Opened Objects")]
        [SerializeField] private Image _levelTextHolder;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _unitView;
        [SerializeField] private Image _unitTypeHolder;
        [SerializeField] private Image _unitTypeIcon;
        [SerializeField] private Button _moreButton;
        [SerializeField] private CardSlider _cardSlider;

        [Header("Closed Objects")]
        [SerializeField] private Image _closedUnitView;
        [SerializeField] private Button _buyButton;

        [Header("Variables")]
        private UnitInfo _unitInfo;
        private UnitDataForSave _unitData;
        private Skin _currentSkin;
        public event Action<UnitDataForSave, UnitLevelsHolder, Skin> OnMoreButtonClicked;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _moreButton.onClick.RemoveListener(More);
            _buyButton.onClick.RemoveListener(BuyCard);
        }

        #endregion

        #region Control Methods

        public void Initialize(UnitInfo unitInfo)
        {
            _unitInfo = unitInfo;
            _unitData = DataSaveManager.Instance.MyData.GetUnitDataMyName(unitInfo.UniqueName);
            _currentSkin = _unitInfo.SkinsHolder.Skins.First(skin => skin.UniqueName == _unitData.CurrentSkinName);

            _levelText.text = $"LVL {_unitData.CurrentLevel}";
            _unitView.sprite = _currentSkin.SkinSprite;
            _unitTypeIcon.sprite = UnitTypeManager.Instance.GetLogoInfoByType(unitInfo.Type);
            _moreButton.onClick.AddListener(More);
            _cardSlider.Initialize(_unitInfo, _unitData);

            _closedUnitView.sprite = _currentSkin.BWSkinSprite;
            _buyButton.onClick.AddListener(BuyCard);

            _unitData.OnCurrentLevelChanged += UpdateCardView;
            UpdateCardView();
        }
        private void More()
        {
            OnMoreButtonClicked?.Invoke(_unitData, _unitInfo.LevelsHolder, _currentSkin);
            PopupManager.Instance.Show("UpgradeUnit");
        }
        private void BuyCard()
        {

        }

        public void UpdateCardView()
        {
            ControlOpenedObjectsVisible(_unitData.CurrentLevel > 0);
            ControlClosedObjectsVisible(_unitData.CurrentLevel == 0);
        }
        private void ControlOpenedObjectsVisible(bool isVisible)
        {
            _levelTextHolder.gameObject.SetActive(isVisible);
            _unitView.gameObject.SetActive(isVisible);
            _unitTypeHolder.gameObject.SetActive(isVisible);
            _moreButton.gameObject.SetActive(isVisible);
            _cardSlider.gameObject.SetActive(isVisible);
        }
        private void ControlClosedObjectsVisible(bool isVisible)
        {
            _closedUnitView.gameObject.SetActive(isVisible);
            _buyButton.gameObject.SetActive(isVisible);
        }

        #endregion
    }
}