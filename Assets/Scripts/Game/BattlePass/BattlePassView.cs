using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Settings;

namespace WOFL.BattlePass
{
    public class BattlePassView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private BattlePassTopBar _topBar;
        [SerializeField] private BattlePassBottomBar _bottomBar;
        [SerializeField] private BattlePassLine _line;

        [Header("Variables")]
        private int _currentSeasonNumber;
        private BattlePassManager.SeasonInfo _currentSeasonInfo;
        private BattlePassDataSave _currentBattlePassDataSave;

        #endregion

        #region Control Methods

        public void Initialize(int seasonNumber, BattlePassManager.SeasonInfo seasonInfo, BattlePassDataSave battlePassDataSave)
        {
            _currentSeasonNumber = seasonNumber;
            _currentSeasonInfo = seasonInfo;
            _currentBattlePassDataSave = battlePassDataSave;

            _topBar.Initialize(_currentSeasonNumber, _currentSeasonInfo, _currentBattlePassDataSave);

            _line.OnLineFilled += CallFillBottomBar;
            _line.Initialize(seasonInfo.BattlePassLine, battlePassDataSave);
        }
        private void CallFillBottomBar()
        {
            _bottomBar.Fill();
        }
        
        #endregion
    }
}