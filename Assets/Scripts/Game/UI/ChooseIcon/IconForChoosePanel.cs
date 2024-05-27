using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Control;
using DG.Tweening;
using Kamen.DataSave;
using Kamen.UI;
using System;

namespace WOFL.UI
{
    [RequireComponent(typeof(KamenButton))]
    public class IconForChoosePanel : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        private KamenButton _button;

        [Header("Settings")]
        [SerializeField] private Color32 _disabledBackgroundColor;
        private Color32 _activateBackgroundColor;
        [SerializeField] private float _switchBackgroundViewDuration;
        [SerializeField] private Ease _switchBackgroundViewEase;

        [Header("Variables")]
        private int _iconNumber;
        public event Action<int> OnClicked;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _button.OnClick().RemoveListener(Click);
        }

        #endregion

        #region Control Methods

        public void Initialize(int iconNumber, Color32 activateBackgroundColor)
        {
            _iconNumber = iconNumber;
            _activateBackgroundColor = activateBackgroundColor;

            _button = GetComponent<KamenButton>();
            _button.Initialize();
            _button.OnClick().AddListener(Click);

            _icon.sprite = FractionManager.Instance.CurrentFraction.PlayerProfileSettings.IconsList[iconNumber];
            SwitchBackgroundView(DataSaveManager.Instance.MyData.IconNumber == iconNumber, true);
        }
        private void Click()
        {
            OnClicked?.Invoke(_iconNumber);
        }
        public void SwitchBackgroundView(bool isActivate, bool isFast = false)
        {
            _background.DOColor(isActivate ? _activateBackgroundColor : _disabledBackgroundColor, isFast ? 0 : _switchBackgroundViewDuration).SetEase(_switchBackgroundViewEase);
        }

        #endregion
    }
}