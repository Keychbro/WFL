using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using WOFL.Control;
using WOFL.Settings;
using WOFL.Save;
using Kamen.DataSave;

namespace WOFL.UI
{
    public class UnitCardInGame : KamenButton
    {
        #region Enums

        public enum CardState
        {
            Closed,
            NoMana,
            Ready
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _unitView;
        [SerializeField] private Image _unitTypeIcon;
        [SerializeField] private TextMeshProUGUI _manaAmountText;

        [Header("Close State Object")]
        [SerializeField] private GameObject _cardClosedView;

        [Header("NoMana State Object")]
        [SerializeField] private UIWall _noManaWall;

        [Header("Variables")]
        private UnitDataForSave _currentUnitData;
        private UnitInfo _currentUnitInfo;
        private CardState _state;

        #endregion

        #region Properties

        public Button.ButtonClickedEvent OnClick { get => _button.onClick; }
        public int CardManaPrice { get; private set; }

        #endregion

        #region Control Merthods

        protected override void Start() {}

        public void Adjust(UnitInfo unitInfo)
        {
            _currentUnitInfo = unitInfo;
            _button = GetComponent<Button>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _currentUnitData = DataSaveManager.Instance.MyData.GetUnitDataMyName(unitInfo.UniqueName);
            Skin currentSkin = _currentUnitInfo.SkinsHolder.Skins.First(skin => skin.UniqueName == _currentUnitData.CurrentSkinName);
            CardManaPrice = unitInfo.LevelsHolder.Levels[_currentUnitData.CurrentLevel].ManaPrice;

            _unitView.sprite = currentSkin.SkinSprite;
            _unitTypeIcon.sprite = UnitTypeManager.Instance.GetLogoInfoByType(unitInfo.Type);
            _manaAmountText.text = CardManaPrice.ToString();

            _state = CardState.Ready;
        }
        public void UpdateCardView(bool isEnoughMana)
        {
            if (_state == CardState.Closed) return;

            if (isEnoughMana && _state != CardState.Ready) Ready();
            else if (!isEnoughMana && _state != CardState.NoMana) NoMana();
            else if (_currentUnitData.CurrentLevel == 0) Close();
        }
        private void Ready()
        {
            _button.interactable = true;
            _noManaWall.Switch(false);
            _state = CardState.Ready;
        }
        private void NoMana()
        {
            _button.interactable = false;
            _noManaWall.Switch(true);
            _state = CardState.NoMana;
        }
        private void Close()
        {
            _button.interactable = false;
            _cardClosedView.SetActive(true);
            _state = CardState.Closed;
        }

        #endregion
    }
}