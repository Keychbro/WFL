using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace Kamen.UI
{
    public class RectBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] protected Image _background;
        [SerializeField] protected Slider _slider;
        [SerializeField] protected Image _fill;
        [SerializeField] protected KamenText _text;

        [Header("Settings")]
        [SerializeField] protected string _symbol;
        [SerializeField] protected DOTweenClassicInfo _fillAnimationInfo;
        [SerializeField] protected DOTweenClassicInfo _paintAnimationInfo;
        [SerializeField] protected BarRange _barRange;

        [Header("Variables")]
        public Action OnFillFinished;

        #endregion

        #region Control Methods

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            _slider.minValue = _barRange.MinValue;
            _slider.maxValue = _barRange.MaxValue;
            _text.SetText(_slider.minValue + _symbol);
        }
        public void Fill(int startValue, int finishValue, bool isFast = false)
        {
            if (finishValue > _barRange.MaxValue) finishValue = _barRange.MaxValue;

            _slider.DOValue(finishValue, _fillAnimationInfo.Duration).SetEase(_fillAnimationInfo.Ease).OnComplete(() =>
            {
                OnFillFinished?.Invoke();
            });
            if (_text.GetGraphicObject() != null) DOVirtual.Int(startValue, finishValue, isFast ? 0 : _fillAnimationInfo.Duration, currentValue => _text.SetText(currentValue + _symbol)).SetEase(_fillAnimationInfo.Ease);
        }
        public void Paint(Color color)
        {
            _fill.DOColor(color, _paintAnimationInfo.Duration).SetEase(_paintAnimationInfo.Ease);
        }

        #endregion
    }
}