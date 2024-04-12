using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;

namespace WOFL.BattlePass
{
    public class BattlePassLevelView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private BattlePassRewardView _classicRewardView;
        [SerializeField] private BattlePassRewardView _forPaidRewardView;
        [SerializeField] private BattlePassLevelPoint _levelPoint;

        //[Header("Variables")]


        #endregion

        #region Control Methods

        public void Initialize(int levelNumber)
        {
            _levelPoint.Initialize(levelNumber, true);
        }

        #endregion
    }
}