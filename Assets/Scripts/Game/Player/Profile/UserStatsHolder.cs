using System.Collections.Generic;
using UnityEngine;
using WOFL.Stats;
using Kamen.DataSave;
using Cysharp.Threading.Tasks;

namespace WOFL.UI
{
    public class UserStatsHolder : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private UserStatsView _statsViewPrefab;

        [Header("Objects")]
        [SerializeField] private Transform _statsHolder;

        [Header("Settings")]
        [SerializeField] private UserStatsInfo[] _statsInfo;

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
            for (int i = 0; i < _statsInfo.Length; i++)
            {
                UserStatsView statsView = Instantiate(_statsViewPrefab, _statsHolder);
                statsView.Initialize(_statsInfo[i], DataSaveManager.Instance.MyData.GetUserStatsDataMyName(_statsInfo[i].StatsName));

                _statsViews.Add(statsView);
            }
        }

        #endregion
    }
}