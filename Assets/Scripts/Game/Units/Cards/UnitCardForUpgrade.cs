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
        [SerializeField] private GameObject _openedUnitViewMask;
        [SerializeField] private Image _levelTextHolder;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _unitView;
        [SerializeField] private Image _unitTypeHolder;
        [SerializeField] private Image _unitTypeIcon;
        [SerializeField] private Button _moreButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private CardSlider _cardSlider;
        [SerializeField] private CardStatsViewer _cardStatsViewer;

        [Header("Closed Objects")]
        [SerializeField] private GameObject _closedUnitViewMask;
        [SerializeField] private Image _closedUnitView;
        [SerializeField] private KamenButton _buyButton;
        [SerializeField] private TextMeshProUGUI _buyButtonText;

        [Header("Variables")]
        private UnitInfo _unitInfo;
        private UnitDataForSave _unitData;
        private Skin _currentSkin;
        public event Action<UnitDataForSave, UnitLevelsHolder, Skin> OnUpgradeButtonClicked;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _moreButton.onClick.RemoveListener(More);
            _buyButton.OnClick().RemoveListener(BuyCard);
        }

        #endregion

        #region Control Methods

        public void Initialize(UnitInfo unitInfo)
        {
            _unitInfo = unitInfo;
            _unitData = DataSaveManager.Instance.MyData.GetUnitDataMyName(unitInfo.UniqueName);
            _currentSkin = _unitInfo.SkinsHolder.Skins.First(skin => skin.UniqueName == _unitData.CurrentSkinName);

            _unitView.sprite = _currentSkin.SkinSprite;
            _unitTypeIcon.sprite = UnitTypeManager.Instance.GetLogoInfoByType(unitInfo.Type);
            _moreButton.onClick.AddListener(More);
            _upgradeButton.onClick.AddListener(Upgrade);
            _cardSlider.Initialize(_unitInfo, _unitData);
            _cardStatsViewer.Initialize(_unitInfo, _unitData);

            _closedUnitView.sprite = _currentSkin.BWSkinSprite;
            _buyButton.Initialize();
            _buyButton.OnClick().AddListener(BuyCard);
            _buyButtonText.text = $"{_unitInfo.LevelsHolder.Levels[0].AmountGoldToUpgrade}";
            DataSaveManager.Instance.MyData.OnGoldAmountChanged += UpdateBuyButtonView;

            _unitData.OnCurrentLevelChanged += UpdateCardView;
            UpdateCardView();
        }
        private void More()
        {
            _cardStatsViewer.SwapViews();
        }
        private void Upgrade()
        {
            OnUpgradeButtonClicked?.Invoke(_unitData, _unitInfo.LevelsHolder, _currentSkin);
            PopupManager.Instance.Show("UpgradeUnit");
        }
        private void BuyCard()
        {
            if (DataSaveManager.Instance.MyData.Gold < _unitInfo.LevelsHolder.Levels[0].AmountGoldToUpgrade) return;

            DataSaveManager.Instance.MyData.Gold -= _unitInfo.LevelsHolder.Levels[0].AmountGoldToUpgrade;
            _unitData.IncreaseLevel();
            DataSaveManager.Instance.SaveData();
        }
        private void UpdateBuyButtonView()
        {
            _buyButton.ChangeInteractable(DataSaveManager.Instance.MyData.Gold >= _unitInfo.LevelsHolder.Levels[0].AmountGoldToUpgrade);
        }

        public void UpdateCardView()
        {
            _levelText.text = $"LVL {_unitData.CurrentLevel}";

            ControlOpenedObjectsVisible(_unitData.CurrentLevel > 0);
            ControlClosedObjectsVisible(_unitData.CurrentLevel == 0);
            _cardStatsViewer.CallUpdateStats();
        }
        private void ControlOpenedObjectsVisible(bool isVisible)
        {
            _openedUnitViewMask.SetActive(isVisible);
            _levelTextHolder.gameObject.SetActive(isVisible);
            _unitView.gameObject.SetActive(isVisible);
            _unitTypeHolder.gameObject.SetActive(isVisible);
            _moreButton.gameObject.SetActive(isVisible);
            _cardSlider.gameObject.SetActive(isVisible);
        }
        private void ControlClosedObjectsVisible(bool isVisible)
        {
            _closedUnitViewMask.SetActive(isVisible);
            _closedUnitView.gameObject.SetActive(isVisible);
            _buyButton.gameObject.SetActive(isVisible);
        }

        #endregion
    }
}