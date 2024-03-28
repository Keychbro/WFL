using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Control;
using WOFL.Game;

namespace WOFL.UI
{
    [RequireComponent(typeof(KamenButton))]
    public class FractionPanel : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _fractionLogo;
        [SerializeField] private Image _nameBackground;
        [SerializeField] private TextMeshProUGUI _nameText;

        [Header("Settings")]
        [SerializeField] private Color32 _inactiveColor;
        [SerializeField] private Color32 _activeColor;
        [SerializeField] private float _switchDuration;
        [SerializeField] private Ease _switchEase;

        [Header("Variables")]
        private KamenButton _button;
        private Fraction _currentFraction;
        public event Action OnFractionPanelClicked;

        #endregion

        #region Control Methods

        public void Initialize(Fraction fraction)
        {
            _currentFraction = fraction;
            _button = GetComponent<KamenButton>();
            _button.OnClick().AddListener(Click);

            _background.color = _inactiveColor;

            _fractionLogo.sprite = _currentFraction.Logo;
            _nameBackground.color = _currentFraction.MainColor;

            _nameText.color = _currentFraction.MainTextColor;
            _nameText.text = _currentFraction.Name.ToString();
        }
        private void Click()
        {
            OnFractionPanelClicked?.Invoke();
        }
        public void SwitchActive(bool isActive)
        {
            _background.DOColor(isActive ? _activeColor : _inactiveColor, _switchDuration).SetEase(_switchEase);
        }
        public Fraction.FractionName GetFractionName() => _currentFraction.Name;

        #endregion
    }
}