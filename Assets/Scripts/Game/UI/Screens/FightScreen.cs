using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using DG.Tweening;
using Kamen;
using Kamen.DataSave;
using WOFL.Game;

namespace WOFL.UI
{
    public class FightScreen : Kamen.UI.Screen
    {
        #region Variables



        #endregion

        #region Control Methods

        public override void Transit(bool isShow, bool isForth, ScreenManager.TransitionType type, float duration, Ease curve, MyCurve myCurve)
        {
            base.Transit(isShow, isForth, type, duration, curve, myCurve);

            if (DataSaveManager.Instance.MyData.ChoosenFraction == Fraction.FractionName.None)
            {
                PopupManager.Instance.Show("ChooseFractionPopup");
            }
        }

        #endregion
    }
}