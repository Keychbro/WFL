using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;
using Kamen;
using Kamen.DataSave;

namespace WOFL.Control
{
    public class FractionManager : SingletonComponent<FractionManager>
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Fraction[] _fractions;

        #endregion

        #region Properties

        public Fraction[] Fractions { get => _fractions; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            InitializeUnitsInBase();
        }

        #endregion

        #region Control Methods

        private void InitializeUnitsInBase()
        {
            for (int i = 0; i < _fractions.Length; i++)
            {
                DataSaveManager.Instance.MyData.AdjustUnitsDatas(_fractions[i].Units);
            }
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}

