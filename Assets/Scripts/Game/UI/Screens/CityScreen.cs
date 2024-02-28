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
        #region Classes

        [Serializable] private struct ResourceInfo
        {
            #region ResourceInfo Variables

            [SerializeField] private Resources _type;
            [SerializeField] private GameObject _field;
            [SerializeField] private TextMeshProUGUI _viewText;

            #endregion

            #region ResourceInfo Properties

            public Resources Type { get => _type; }
            public GameObject Field { get => _field; }
            public TextMeshProUGUI ViewText { get => _viewText; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private ResourceInfo[] _resourceInfos;
        [SerializeField] private Image _cityView;

        [Header("Settings")]
        [SerializeField] private CityLevelInfo[] _levelInfos;

        [Header("Variables")]
        private CityLevelInfo _currentLevelInfo;

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

            _cityView.sprite = _currentLevelInfo.CityView;
            for (int i = 0; i < _resourceInfos.Length; i++)
            {
                ResourceHandler(_resourceInfos[i], _currentLevelInfo.ResourceProduceInfos.First(resourceProduceInfo => resourceProduceInfo.Type == _resourceInfos[i].Type));
            }
        }
        private void ResourceHandler(ResourceInfo resourceInfo, CityLevelInfo.ResourceProduceInfo resourceProduceInfo)
        {
            if (resourceProduceInfo.ProducePerSeconds == 0)
            {
                resourceInfo.Field.SetActive(false);
                return;
            }

            resourceInfo.Field.SetActive(true);
            resourceInfo.ViewText.text = $"+ {resourceProduceInfo.ProducePerSeconds} / s";
        }

        #endregion
    }
}