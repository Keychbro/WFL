using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.UI
{
    public class RewardStateView : MonoBehaviour
    {
        #region Enum

        public enum RewardState
        {
            Closed,
            Ready,
            Accepted
        }

        #endregion

        #region Classes

        [Serializable] protected class StateViewInfo
        {
            #region StateViewInfo Variables

            [SerializeField] private RewardState _state;
            [SerializeField] private AnimateIcon _icon;

            #endregion

            #region StateViewInfo Properties

            public RewardState State { get => _state; }
            public AnimateIcon Icon { get => _icon; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private StateViewInfo[] _stateViewInfos;

        [Header("Varriables")]
        private StateViewInfo _currentViewInfo;

        #endregion

        #region Control Methods

        public void SwitchState(RewardState rewardState)
        {
            if (_currentViewInfo != null)
            {
                if (_currentViewInfo.Icon != null) _currentViewInfo.Icon.CallDissapear();
            }

            _currentViewInfo = _stateViewInfos.First(stateViewInfo => stateViewInfo.State == rewardState);
            if (_currentViewInfo.Icon != null) _currentViewInfo.Icon.CallAppear();
        }

        #endregion
    }
}