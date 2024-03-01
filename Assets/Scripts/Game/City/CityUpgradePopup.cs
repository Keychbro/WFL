using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using TMPro;
using WOFL.Settings;
using System.Linq;
using WOFL.Control;
using Kamen.DataSave;

namespace WOFL.UI
{
    public class CityUpgradePopup : Popup
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _levelNumber;
        [SerializeField] private ResourceInfo[] _resourceInfos;
        [SerializeField] private Image _cityView;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private KamenButton _button;

        [Header("Variables")]
        private CityLevelInfo _levelInfo;

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();
            _button.Initialize();
            _button.OnClick().AddListener(UpgradeCity);
        }
        public override void Show()
        {
            base.Show();
            _button.ChangeInteractable(_levelInfo.Price <= DataSaveManager.Instance.MyData.Tools);
        }
        public void UpdateView(CityLevelInfo levelInfo, int number)
        {
            _levelInfo = levelInfo;

            _levelNumber.text = $"LEVEL {number}";
            _cityView.sprite = levelInfo.CityView;
            for (int i = 0; i < _resourceInfos.Length; i++)
            {
                _resourceInfos[i].ResourceHandler(levelInfo.ResourceProduceInfos.First(resourceProduceInfo => resourceProduceInfo.Type == _resourceInfos[i].Type));
            }

            _priceText.text = BigNumberViewConverter.Instance.Convert(levelInfo.Price);
        }
        private void UpgradeCity()
        {
            if (_levelInfo.Price > DataSaveManager.Instance.MyData.Tools) return;

            DataSaveManager.Instance.MyData.Tools -= _levelInfo.Price;
            DataSaveManager.Instance.MyData.CityLevel++;
            PopupManager.Instance.HideAllPopups();
        }

        #endregion
    }
}