using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using DG.Tweening;
using Kamen;
using DG.Tweening;
using TMPro;
using System.Threading.Tasks;

namespace WOFL.UI
{
    public class LoadingScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private Slider _loadingBar;
        [SerializeField] private TextMeshProUGUI _loadingText;

        [Header("Settings")]
        [SerializeField] private Sprite[] _backgroundViews;
        [SerializeField] private string _sliderText;
        [SerializeField] private string _valueSign;
        [Space]
        [SerializeField] private float _loadDuration;
        [SerializeField] private float _delayAfterLoad;
        [SerializeField] private Ease _loadEase;

        #endregion

        #region Control Methods

        public override void Transit(bool isShow, bool isForth, ScreenManager.TransitionType type, float duration, Ease curve, MyCurve myCurve)
        {
            base.Transit(isShow, isForth, type, duration, curve, myCurve);

            _background.sprite = _backgroundViews[Random.Range(0, _backgroundViews.Length)];
            LoadingBarFilling();
        }
        private async void LoadingBarFilling()
        {
            _loadingBar.DOValue(100, _loadDuration).SetEase(_loadEase);
            DOVirtual.Int(0, 100, _loadDuration, UpdateLoadingText);

            await Task.Delay(Mathf.RoundToInt(_loadDuration * 1000));
            await Task.Delay(Mathf.RoundToInt(_delayAfterLoad * 1000));

            ScreenManager.Instance.TransitionTo("Fight");
        }
        private void UpdateLoadingText(int value) => _loadingText.text = $"{_sliderText}{value}{_valueSign}";

        #endregion
    }
}