using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using Cysharp.Threading.Tasks;
using Kamen.DataSave;
using WOFL.Game;
using WOFL.Control;

namespace WOFL.UI
{
    public class ChooseIconPopup : Popup
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private IconForChoosePanel _iconForChoosePanelPrefab;

        [Header("Objects")]
        [SerializeField] private Transform _iconsHolder;
        [SerializeField] private KamenButton _closeButton;
        [SerializeField] private KamenButton _saveButton;

        [Header("Variables")]
        private int _choosenIconNumber;
        private List<IconForChoosePanel> _iconForChoosePanels = new List<IconForChoosePanel>();

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _closeButton.OnClick().RemoveListener(JustClosePopup);
            _saveButton.OnClick().RemoveListener(SavePopup);
        }

        #endregion

        #region Control Methods

        public async override void Initialize()
        {
            base.Initialize();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            await UniTask.WaitUntil(() => DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None);

            for (int i = 0; i < FractionManager.Instance.CurrentFraction.PlayerProfileSettings.IconsList.Length; i++)
            {
                CreateIconForChoosePanel(i, FractionManager.Instance.CurrentFraction.MainColor);
            }

            _choosenIconNumber = DataSaveManager.Instance.MyData.IconNumber;

            _closeButton.Initialize();
            _closeButton.OnClick().AddListener(JustClosePopup);

            _saveButton.Initialize();
            _saveButton.OnClick().AddListener(SavePopup);
        }
        private void CreateIconForChoosePanel(int iconNumber, Color32 activateBackgroundColor)
        {
            IconForChoosePanel iconForChoosePanel = Instantiate(_iconForChoosePanelPrefab, _iconsHolder);
            iconForChoosePanel.Initialize(iconNumber, activateBackgroundColor);
            iconForChoosePanel.OnClicked += UpdateChoosenIconView;

            _iconForChoosePanels.Add(iconForChoosePanel);
        }
        private void UpdateChoosenIconView(int iconNumber)
        {
            _iconForChoosePanels[_choosenIconNumber].SwitchBackgroundView(false);
            _iconForChoosePanels[iconNumber].SwitchBackgroundView(true);

            _choosenIconNumber = iconNumber;
        }
        private void JustClosePopup()
        {
            _choosenIconNumber = DataSaveManager.Instance.MyData.IconNumber;
            PopupManager.Instance.Hide("ChooseIconPopup");
        }
        private void SavePopup()
        {
            DataSaveManager.Instance.MyData.IconNumber = _choosenIconNumber;
            PopupManager.Instance.Hide("ChooseIconPopup");
        }

        #endregion
    }
}