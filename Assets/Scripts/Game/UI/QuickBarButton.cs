using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Settings;
using System.Linq;

namespace WOFL.UI
{
    [RequireComponent(typeof(Button))]
    public class QuickBarButton : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _icon;
        [SerializeField] private Image _background;
        [SerializeField] private Image _indicator;

        [Header("Settings")]
        [SerializeField] private QuickBarButtonInfo[] _buttonsViewInfo;

        [Header("Variables")]
        private Button _button;
        private QuickBarButtonInfo.ButtonState _currentState;
        public event Action<QuickBarButton> OnClicked;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Click);
        }

        #endregion

        #region Control Methods

        public void Initialize()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }
        private void Click()
        {;
            OnClicked?.Invoke(this);
        }
        public void CallChangeButtonState(QuickBarButtonInfo.ButtonState state, bool isFast = false)
        {
            Animate(GetQuickBarButtonInfoByState(state).ViewInfo, isFast);
        }
        private void Animate(QuickBarButtonInfo.ButtonViewInfo viewInfo, bool isFast)
        {
            _icon.DOColor(viewInfo.MainColor, isFast ? 0 : viewInfo.AnimationDuration).SetEase(viewInfo.AnimationEase);
            _background.DOColor(viewInfo.Background, isFast ? 0 : viewInfo.AnimationDuration).SetEase(viewInfo.AnimationEase);

            _indicator?.DOColor(viewInfo.MainColor, isFast ? 0 : viewInfo.AnimationDuration).SetEase(viewInfo.AnimationEase);
            _indicator?.gameObject.transform.DOScale(viewInfo.IndicatorObject.ObjectScale, isFast ? 0 : viewInfo.IndicatorObject.AnimationDuration).
                                            SetEase(viewInfo.IndicatorObject.AnimationEase);
        }

        #endregion

        #region Calculation Methods

        private QuickBarButtonInfo GetQuickBarButtonInfoByState(QuickBarButtonInfo.ButtonState state)
        {
            return _buttonsViewInfo.First(number => number.State == state);
        }

        #endregion
    }
}