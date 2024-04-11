using Cysharp.Threading.Tasks;
using Kamen;
using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.Control
{
    public class BattlePassManager : SingletonComponent<BattlePassManager>
    {
        #region Classes

        [Serializable] public class SeasonInfo
        {
            #region SeasonInfo Variables

            [SerializeField] private DateTimeConstructor _startSeasonDate;
            [SerializeField] private DateTimeConstructor _finishSeasonDate;
            [SerializeField] private BattlePassLineData _battlePassLine;

            #endregion

            #region SeasonInfo Properties

            public DateTimeConstructor StartSeasonDate { get => _startSeasonDate; }
            public DateTimeConstructor FinishSeasonDate { get => _finishSeasonDate; }
            public BattlePassLineData BattlePassLine { get => _battlePassLine; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private SeasonInfo[] _seasonInfos;

        #endregion

        #region Properties

        public SeasonInfo CurrentSeasonInfo { get; private set; }
        public int CurrentSeasonNumber { get; private set; }

        #endregion

        #region Unity Methods

        protected async override void Awake()
        {
            base.Awake();
            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);

            DataSaveManager.Instance.MyData.AdjustBattlePassDatas(_seasonInfos);
            DataSaveManager.Instance.SaveData();

            CurrentSeasonInfo = null;
            CalculateCurrentBattlePassSeason();
        }

        #endregion

        #region Control Methods

        public void CalculateCurrentBattlePassSeason()
        {
            for (int i = 0; i < _seasonInfos.Length; i++)
            {
                if (CheckSeasonActive(_seasonInfos[i].StartSeasonDate, _seasonInfos[i].FinishSeasonDate))
                {
                    CurrentSeasonInfo = _seasonInfos[i];
                    CurrentSeasonNumber = i + 1;
                    break;
                }
            }
        }
        private bool CheckSeasonActive(DateTimeConstructor startDate, DateTimeConstructor finishDate)
        {
            DateTime startDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            DateTime finishDateTime = new DateTime(finishDate.Year, finishDate.Month, finishDate.Day);

            return startDateTime <= DateTime.Now && finishDateTime > DateTime.Now;
        }

        #endregion
    }
}