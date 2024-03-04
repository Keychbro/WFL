using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Control
{
    [RequireComponent(typeof(Button), typeof(CanvasGroup))]
    public class KamenButton : MonoBehaviour
    {
        #region Variables

        [Header("Variables")]
        protected Button _button;
        protected CanvasGroup _canvasGroup;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            Initialize();
        }

        #endregion

        #region Control Methods

        public virtual void Initialize()
        {
            _button = GetComponent<Button>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public Button.ButtonClickedEvent OnClick() => _button.onClick;
        public void ChangeInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
            _canvasGroup.alpha = isInteractable ? 1 : 0.4f;
        }

        #endregion
    }
}