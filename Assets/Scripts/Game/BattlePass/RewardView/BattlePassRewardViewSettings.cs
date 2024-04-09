using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.UI;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Battle Pass Reward View Settings", menuName = "WOFL/BattlePass/View/Battle Pass Reward View Settings", order = 1)]
    public class BattlePassRewardViewSettings : ScriptableObject
    {
        #region Classes

        [Serializable] public class ViewSettingsInfo
        {
            #region ViewSettingsInfo Variables

            [SerializeField] private RewardStateView.RewardState _state;
            [SerializeField] private Color32 _color;

            #endregion

            #region ViewSettings Properties

            public RewardStateView.RewardState State { get => _state; }
            public Color32 Color { get => _color; }
            
            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private ViewSettingsInfo[] _viewSettingsInfos;

        #endregion

        #region Properties

        public ViewSettingsInfo[] ViewSettingsInfos { get => _viewSettingsInfos;  }

        #endregion
    }

}