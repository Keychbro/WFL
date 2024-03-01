using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "City Level Info", menuName = "WOFL/Settings/City Level Info", order = 1)]
    public class CityLevelInfo : ScriptableObject
    {
        #region Classes

        [Serializable] public class ResourceProduceInfo
        {
            #region ResourceProduceInfo Variables

            [SerializeField] private Resources _type;
            [SerializeField] private float _producePerSeconds;

            #endregion

            #region ResourceProduceInfo Properties

            public Resources Type { get => _type; }
            public float ProducePerSeconds { get => _producePerSeconds; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("View Settings")]
        [SerializeField] private Sprite _cityView;
        [Header("Level Up Settings")]
        [SerializeField] private int _price;
        [SerializeField] private int _amountCompleteLevelsToLevelUp;

        [Header("Produce Settings")]
        [SerializeField] private ResourceProduceInfo[] _resourceProduceInfos;

        #endregion

        #region Properties

        public Sprite CityView { get => _cityView; }

        public int Price { get => _price; }
        public int AmountCompleteLevelsToLevelUp { get => _amountCompleteLevelsToLevelUp; }

        public ResourceProduceInfo[] ResourceProduceInfos { get => _resourceProduceInfos; }

        #endregion
    }
}