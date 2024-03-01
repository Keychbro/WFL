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

        [Header("Variables")]
        private CityLevelInfo _currentLevelInfo;
        private CityLevelInfo _nextLevelInfo;

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();

            DataSaveManager.Instance.MyData.OnCityLevelChanged += UpdateCity;
            UpdateCity();

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