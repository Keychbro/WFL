using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using WOFL.Settings;
using Kamen.DataSave;
using System;
using TMPro;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace WOFL.UI
{
    public class CityScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _cityView;
        [SerializeField] private ResourceInfo[] _resourceInfos;
        [Space]
        [SerializeField] private CityLevelUpButton _levelUpButton;
        [SerializeField] private CityUpgradePopup _upgradePopup;

        [Header("Settings")]
        [SerializeField] private CityLevelInfo[] _levelInfos;
        [SerializeField] private int _collectDelay;

        [Header("Variables")]
        private CityLevelInfo _currentLevelInfo;
        private CityLevelInfo _nextLevelInfo;
        private bool _isGameWorking = true;
        public event Action<int> OnTimeToCollectUpdate;

        #endregion

        #region Unity Methods

        private void OnApplicationQuit()
        {
            _isGameWorking = false;
        }

        #endregion

        #region Control Methods

        public async override void Initialize()
        {
            base.Initialize();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);

            DataSaveManager.Instance.MyData.OnCityLevelChanged += UpdateCity;
            UpdateCity();
            CollectResources();
        }
        private async void CollectResources()
        {
            while (_isGameWorking)
            {
                for (int i = 0; i < _collectDelay; i++)
                {
                    await Task.Delay(Mathf.RoundToInt(1000));
                    OnTimeToCollectUpdate?.Invoke(_collectDelay - i);
                }
                for (int i = 0; i < _currentLevelInfo.ResourceProduceInfos.Length; i++)
                {
                    switch (_currentLevelInfo.ResourceProduceInfos[i].Type)
                    {
                        case Resources.Gold:
                            DataSaveManager.Instance.MyData.Gold += Mathf.RoundToInt(_currentLevelInfo.ResourceProduceInfos[i].ProducePerSeconds * _collectDelay);
                            break;
                        case Resources.Diamonds:
                            DataSaveManager.Instance.MyData.Diamonds += Mathf.RoundToInt(_currentLevelInfo.ResourceProduceInfos[i].ProducePerSeconds * _collectDelay);
                            break;
                        case Resources.Tools:
                            DataSaveManager.Instance.MyData.Tools += Mathf.RoundToInt(_currentLevelInfo.ResourceProduceInfos[i].ProducePerSeconds * _collectDelay);
                            break;
                    }
                }
                DataSaveManager.Instance.SaveData();
            }
        }
        private void UpdateCity()
        {
            _currentLevelInfo = _levelInfos[DataSaveManager.Instance.MyData.CityLevel];
            _nextLevelInfo = _levelInfos[DataSaveManager.Instance.MyData.CityLevel < _levelInfos.Length ? DataSaveManager.Instance.MyData.CityLevel + 1: ^1];

            _cityView.sprite = _currentLevelInfo.CityView;
            for (int i = 0; i < _resourceInfos.Length; i++)
            {
                _resourceInfos[i].ResourceHandler(_currentLevelInfo.ResourceProduceInfos.First(resourceProduceInfo => resourceProduceInfo.Type == _resourceInfos[i].Type));
            }

            _levelUpButton.UpdateButtonView(DataSaveManager.Instance.MyData.CityLevel, DataSaveManager.Instance.MyData.CityLevel == _levelInfos.Length);
            _upgradePopup.UpdateView(_nextLevelInfo, DataSaveManager.Instance.MyData.CityLevel + 1);
        }

        #endregion
    }
}