using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using WOFL.Game;
using WOFL.Control;
using Cysharp.Threading.Tasks;
using Kamen.DataSave;

namespace WOFL.UI
{
    public class UpgradeScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private UpgradeCardsHolder _upgradeCardsHolder;

        #endregion

        #region Contorl Methods

        public async override void Initialize()
        {
            base.Initialize();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);

            _upgradeCardsHolder.Initialize(FractionManager.Instance.CurrentFraction.Units);
        }

        #endregion
    }
}