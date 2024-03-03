using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;
using Kamen;

namespace WOFL.Control
{
    public class FractionManager : SingletonComponent<FractionManager>
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Fraction[] _fractions;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
        }

        #endregion

        #region Control Methods

        private void InitializeUnitsInBase()
        {

        }

        #endregion
    }
}

