using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using TMPro;
using WOFL.Game;
using System;
using WOFL.Control;

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
        private List<EndGameRewardView> _rewardViews = new List<EndGameRewardView>();

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _increaseRewardsButton.Button.onClick.RemoveListener(TryIncreaseReward);
            _continueButton.Button.onClick.RemoveListener(Continue);
        }

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();

            _increaseRewardsButton.Button.onClick.AddListener(TryIncreaseReward);
            _continueButton.Button.onClick.AddListener(Continue);
        }

        private List<EndGameRewardInfo> chachedRewardInfos;
        public void AdjustRewards(List<EndGameRewardInfo> rewardInfos)
        {
            chachedRewardInfos = rewardInfos;
            for (int i = 0; i < _rewardViews.Count; i++)
            {
                Destroy(_rewardViews[i].gameObject);
            }
            _rewardViews.Clear();

            for (int i = 0; i < rewardInfos.Count; i++)
            {
                CreateEndGameReward(rewardInfos[i]);
            }
        }

        public void IncreaseAndAdjustChachedReward()
        {
            TryIncreaseReward();
            AdjustRewards(chachedRewardInfos);
        }

        public void CreateEndGameReward(EndGameRewardInfo rewardInfo)
        {
            EndGameRewardView rewardView = Instantiate(_endGameRewardViewPrefab, _rewardHolder.transform);
            rewardView.Initialize(rewardInfo);

            _rewardViews.Add(rewardView);
        }
        public void TryIncreaseReward()
        {
            //TODO Add

            IncreaseRewards();
        }
        public void IncreaseRewards()
        {
            for (int i = 0; i < _rewardViews.Count; i++)
            {
                _rewardViews[i].CurrentRewardInfo.IncreaseByFactorValue(_increaseRewardFactor);
            }
        }
        public void Continue()
        {
            GameManager.Instance.FinishBattle();
            
        }

        #endregion
    }
}