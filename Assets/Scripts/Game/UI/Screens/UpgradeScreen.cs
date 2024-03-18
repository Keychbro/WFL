using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using WOFL.Game;
using WOFL.Control;

namespace WOFL.UI
{
    public class UpgradeScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private UpgradeCardsHolder _upgradeCardsHolder;

        #endregion

        #region Contorl Methods

        public override void Initialize()
        {
            base.Initialize();
            _upgradeCardsHolder.Initialize(FractionManager.Instance.CurrentFraction.Units);
        }

        #endregion
    }
}