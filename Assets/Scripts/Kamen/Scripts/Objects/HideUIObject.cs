using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Kamen.Settings;
using System;

namespace Kamen.Objects
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HideUIObject : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private HideUISettings _hideUISettings;
        [SerializeField] private HideUISettings _showUISettings;

        [Header("Varibles")]
        private CanvasGroup _canvasGroup;
        public Action OnHided;
        public Action OnShowen;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        public void Hide(bool isFast = false)
        {
            _canvasGroup.DOFade(_hideUISettings.Value, isFast ? 0 : _hideUISettings.Duration).SetEase(_hideUISettings.Ease).SetDelay(_hideUISettings.Delay).OnComplete(() =>
            {
                if (_hideUISettings.IsDeactivate) _canvasGroup.gameObject.SetActive(false);
                OnHided?.Invoke();
            });
        }
        public void Show(bool isFast = false)
        {
            if (_hideUISettings.IsDeactivate) _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.DOFade(_showUISettings.Value, isFast ? 0 : _showUISettings.Duration).SetEase(_showUISettings.Ease).SetDelay(_showUISettings.Delay).OnComplete(() =>
            {
                OnShowen?.Invoke();
            });
        }

        #endregion
    }
}