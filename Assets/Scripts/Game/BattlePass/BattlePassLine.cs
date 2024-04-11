using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Settings;
using WOFL.UI;

namespace WOFL.BattlePass
{
    public class BattlePassLine : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private BattlePassRewardView _battlePassRewardViewPrefab;

        [Header("Objects")]
        [SerializeField] private Slider _scoreLine;

        #endregion

        #region Control Methods

        public void Initialize()
        {

        }

        #endregion
    }
}