using System.Collections.Generic;
using UnityEngine;
using WOFL.Stats;
using Kamen.DataSave;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

namespace WOFL.UI
{
    public class UserStatsHolder : MonoBehaviour
    {
        #region Classes

        [Serializable] private class MainStatsInfo
        {
            #region Variables

            [SerializeField] private UserStatsView _view;
            [SerializeField] private string _name;

            #endregion

            #region Properties

            public UserStatsView View { get => _view; }
            public string Name { get => _name; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Prefabs")]
        [SerializeField] private UserStatsView _statsViewPrefab;

        [Header("Objects")]
        [SerializeField] private Transform _statsHolder;

        [Header("Settings")]
        [SerializeField] private UserStatsInfo[] _statsInfo;
        [SerializeField] private MainStatsInfo[] _mainStatsInfo;

        [Header("Variables")]
        private List<UserStatsView> _statsViews = new List<UserStatsView>();

        #endregion

        #region Unity Methods

        private async void Awake()
        {
            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);

            DataSaveManager.Instance.MyData.AdjustUserStatsDatas(_statsInfo);
            DataSaveManager.Instance.SaveData();
            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            List<UserStatsInfo> usedStatsInfos = new List<UserStatsInfo>();
            usedStatsInfos.AddRange(_statsInfo);
            
            for (int i = 0; i < _mainStatsInfo.Length; i++)
            {
                UserStatsInfo currentStatsInfo = usedStatsInfos.First(info => info.StatsName == _mainStatsInfo[i].Name);
                usedStatsInfos.Remove(currentStatsInfo);

                _mainStatsInfo[i].View.Initialize(currentStatsInfo, DataSaveManager.Instance.MyData.GetUserStatsDataMyName(currentStatsInfo.StatsName));
            }

            for (int i = 0; i < usedStatsInfos.Count; i++)
            {
                UserStatsView statsView = Instantiate(_statsViewPrefab, _statsHolder);
                statsView.Initialize(usedStatsInfos[i], DataSaveManager.Instance.MyData.GetUserStatsDataMyName(usedStatsInfos[i].StatsName));

                _statsViews.Add(statsView);
            }
        }

        #endregion
    }
}