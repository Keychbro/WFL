using DG.Tweening;
using Kamen.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace WOFL.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MainTopBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private ScreenManager _screenManager;
        private CanvasGroup _canvasGroup;

        [Header("Settings")]
        [SerializeField] private Ease _switchVisibleEase;

        [Header("Variables")]
        private bool _isVisible = true;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _screenManager.OnScreenChanged += UpdateBarView;
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        private void UpdateBarView(ScreenManager.ScreenInfo screenInfo, bool isFast)
        {
            SwitchVisible(screenInfo.IsShowTopBar, isFast);
        }
        private async void SwitchVisible(bool isShow, bool isFast)
        {
            if (_isVisible == isShow) return;

            if (isShow)
            {
                gameObject.SetActive(true);
                _canvasGroup.alpha = 0;
            }

            _canvasGroup.DOFade(isShow ? 1 : 0, isFast ? 0 : _screenManager.TransitionDuration).SetEase(_switchVisibleEase);
            await Task.Delay(Mathf.RoundToInt(1000 * (isFast ? 0 : _screenManager.TransitionDuration)));

            if (!isShow) gameObject.SetActive(false);
            _isVisible = isShow;
        }

        #endregion
    }
}