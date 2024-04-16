using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using TMPro;
using WOFL.Game;
using System;

namespace WOFL.UI
{
    public class EndGameScreenPopup : Popup
    {
        #region Classes

        [Serializable] private class EndGameButtonInfo
        {
            #region Variables

            [SerializeField] private Button _button;
            [SerializeField] private TextMeshProUGUI _text;

            #endregion

            #region Properties

            public Button Button { get => _button; }
            public TextMeshProUGUI Text { get => _text;  }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Prefabs")]
        [SerializeField] private EndGameRewardView _endGameRewardViewPrefab;

        [Header("Objects")]
        [SerializeField] private GameObject _rewardHolder;
        [Space]
        [SerializeField] private EndGameButtonInfo _increaseRewardsButton;
        [SerializeField] private EndGameButtonInfo _continueButton;
        [SerializeField] private TextMeshProUGUI _ratingValueText;

        [Header("Settings")]
        [SerializeField] private float _increaseRewardFactor;

        [Header("Variables")]
        [SerializeField] private List<EndGameRewardView> _rewardViews = new List<EndGameRewardView>();

        #endregion

        #region Control Methods

        public void Initialize(List<EndGameRewardInfo> rewardInfos)
        {
            for (int i = 0; i < rewardInfos.Count; i++)
            {
                CreateEndGaemReward(rewardInfos[i]);
            }
        }
        public void CreateEndGaemReward(EndGameRewardInfo rewardInfo)
        {
            EndGameRewardView rewardView = Instantiate(_endGameRewardViewPrefab, _rewardHolder.transform);
            rewardView.Initialize(rewardInfo);

            _rewardViews.Add(rewardView);
        }
        public void IncreaseRewards()
        {

        }
        public void Continue()
        {

        }

        #endregion
    }
}