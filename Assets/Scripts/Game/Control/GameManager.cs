using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Game;
using System.Linq;
using Kamen.DataSave;
using WOFL.UI;

namespace WOFL.Control
{
    public class GameManager : SingletonComponent<GameManager>
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Castle _alliedCastle;
        [SerializeField] private Castle _enemyCastle;
        [Space]
        [SerializeField] private ManaView _manaView;

        [Header("Variables")]
        private Fraction _playerfraction;

        #endregion

        #region Unity Methods

        private void Start()
        {
            //_playerfraction = FractionManager.Instance.Fractions
            //    .First(fraction => fraction.Name == DataSaveManager.Instance.MyData.ChoosenFraction);

            _playerfraction = FractionManager.Instance.Fractions[0];
            StartBattle();
        }

        #endregion

        #region Control Methods

        public void StartBattle()
        {     
            _alliedCastle.Initialize(_playerfraction.CastleSettings, _playerfraction.Units);
            _manaView.Initialize(_alliedCastle);
        }

        #endregion
    }
}