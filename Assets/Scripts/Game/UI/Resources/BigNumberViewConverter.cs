using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using System;

namespace WOFL.Control
{
    public class BigNumberViewConverter : SingletonComponent<BigNumberViewConverter>
    {
        #region Classes

        [Serializable] private struct BigNumberInfo
        {
            #region Variables

            [SerializeField] private float _roundingBigNumber;
            [SerializeField] private int _numberDecimalPlaces;
            [SerializeField] private string _sign;

            #endregion

            #region Properties

            public float RoundingBigNumber { get => _roundingBigNumber; }
            public int NumberDecimalPlaces { get => _numberDecimalPlaces; }
            public string Sign { get => _sign; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private BigNumberInfo _thousandNumberInfo;
        [SerializeField] private BigNumberInfo _millionNumberInfo;

        [Header("Variables")]
        private string _numberView;

        #endregion

        #region Control Methods

        public string Convert(int number) => Convert((float)number);
        public string Convert(float number)
        {
            if (number < _thousandNumberInfo.RoundingBigNumber)
            {
                return number.ToString();
            }
            else if (number >= _thousandNumberInfo.RoundingBigNumber && number < _millionNumberInfo.RoundingBigNumber)
            {
                return (Math.Round(number / _thousandNumberInfo.RoundingBigNumber, _thousandNumberInfo.NumberDecimalPlaces) + _thousandNumberInfo.Sign);
            }
            else if (number >= _millionNumberInfo.RoundingBigNumber)
            {
                return (Math.Round(number / _millionNumberInfo.RoundingBigNumber, _millionNumberInfo.NumberDecimalPlaces) + _millionNumberInfo.Sign);
            }
            else return number.ToString();
        }

        #endregion
    }
}