using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "City Level Info", menuName = "WOFL/Settings/City Level Info", order = 1)]
    public class CityLevelInfo : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Sprite _cityView;
        [SerializeField] private float _produceGoldPerSeconds;
        [SerializeField] private float _produceDimondsPerSeconds;
        [SerializeField] private float _produceToolsPerSeconds;
        [SerializeField] private int _amountToolsToUpgrade;

        #endregion

        #region Properties

        public Sprite CityView { get => _cityView; }
        public float ProduceGoldPerSeconds { get => _produceGoldPerSeconds; }
        public float ProduceDiamondsPerSeconds { get => _produceDimondsPerSeconds; }
        public float ProduceToolsPerSeconds { get => _produceToolsPerSeconds; }
        public int AmountToolsToUpgrade { get => _amountToolsToUpgrade; }

        #endregion
    }
}