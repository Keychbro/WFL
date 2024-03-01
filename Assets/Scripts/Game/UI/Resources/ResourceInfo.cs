using UnityEngine;
using System;
using TMPro;
using WOFL.Settings;
using WOFL.Control;

namespace WOFL.UI
{
    [Serializable]
    public struct ResourceInfo
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

        #region Control Methods

        public void ResourceHandler(CityLevelInfo.ResourceProduceInfo resourceProduceInfo)
        {
            if (resourceProduceInfo.ProducePerSeconds == 0)
            {
                Field.SetActive(false);
                return;
            }

            Field.SetActive(true);
            ViewText.text = $"+ {BigNumberViewConverter.Instance.Convert(resourceProduceInfo.ProducePerSeconds)} / s";
        }

        #endregion
    }
}