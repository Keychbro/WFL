using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using DG.Tweening;
using Kamen;
using System.Threading.Tasks;
using System;

namespace WOFL.Control
{
    public class RegistrationScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _signUpPopupName;
        [SerializeField] private string _signInPopupName;

        #endregion

        #region Control Methods

        public override void ShowCanvasGroup()
        {
            base.ShowCanvasGroup();
            ShowSignUpPopup(0);
        }
        public override void Transit(bool isShow, bool isForth, ScreenManager.TransitionType type, float duration, Ease curve, MyCurve myCurve)
        {
            base.Transit(isShow, isForth, type, duration, curve, myCurve);
            if (isShow) ShowSignUpPopup(duration);
        }
        private async void ShowSignUpPopup(float duration)
        {
            await Task.Delay(Mathf.RoundToInt(duration * 1000));
            PopupManager.Instance.Show(_signUpPopupName);
        }
        public void FinishRegistration()
        {
            ScreenManager.Instance.TransitionTo("ChooseServer");
            PopupManager.Instance.HideAllPopups();
        }

        #endregion
    }
}

