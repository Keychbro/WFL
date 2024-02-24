using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kamen.UI
{
    public class RoundBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] protected Image _background;
        [SerializeField] protected Image _fillPart;
        [SerializeField] protected KamenText _text;

        [Header("Settings")]
        [SerializeField] protected string _symbol;
        [SerializeField] protected DOTweenClassicInfo _animationInfo;
        [SerializeField] protected BarRange _barRange;

        [Header("Variables")]
        public Action OnFillFinished;

        #endregion

        #region Control Methods

        public virtual void Fill(int startValue, int finishValue, bool isFast = false)
        {
            if (finishValue > _barRange.MaxValue) finishValue = _barRange.MaxValue;

            _fillPart.fillAmount = startValue / _barRange.MaxValue;
            _fillPart.DOFillAmount(finishValue / _barRange.MaxValue, isFast ? 0 : _animationInfo.Duration).SetEase(_animationInfo.Ease).OnComplete(() =>
            {
                OnFillFinished?.Invoke();
            });
            WriteText(startValue, finishValue);
        }
        protected virtual void WriteText(int startValue, int finishValue)
        {
            if (_text.GetGraphicObject() != null)
            {
                DOVirtual.Int(startValue, finishValue, _animationInfo.Duration, currentValue => _text.SetText(currentValue + _symbol)).SetEase(_animationInfo.Ease);
            }
        }
        
        
        #endregion
    }
}