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
        [SerializeField] private BattlePassLevelView _battlePassLevelViewPrefab;

        [Header("Objects")]
        [SerializeField] private Slider _scoreLine;
        [SerializeField] private GameObject _levelHolder;

        [Header("Variables")]
        private List<BattlePassLevelView> _battlePassLevels = new List<BattlePassLevelView>();

        #endregion

        #region Control Methods

        public void Initialize(BattlePassRewardInfo[] classicLineDatas, BattlePassRewardInfo[] forPaidLineDatas)
        {
            BattlePassRewardInfo[] greatestLength = classicLineDatas.Length >= forPaidLineDatas.Length ? classicLineDatas : forPaidLineDatas;
            for (int i = 0; i < greatestLength.Length; i++)
            {
                BattlePassLevelView newBattlePassLevel = Instantiate(_battlePassLevelViewPrefab, _levelHolder.transform);
                newBattlePassLevel.Initialize(i + 1);
                _battlePassLevels.Add(newBattlePassLevel);
            }
        }

        #endregion
    }
}