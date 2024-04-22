using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;
using Kamen;
using Kamen.DataSave;
using System.Linq;
using Cysharp.Threading.Tasks;


namespace WOFL.Control
{
    public class FractionManager : SingletonComponent<FractionManager>
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Fraction[] _fractions;

        [Header("Variables")]
        private Fraction _currentFraction;

        #endregion

        #region Properties

        public Fraction[] Fractions { get => _fractions; }
        public Fraction CurrentFraction { get => _currentFraction; }

        #endregion

        #region Unity Methods

        protected async override void Awake()
        {
            base.Awake();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            await UniTask.WaitUntil(() => DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None);

            InitializeUnitsInBase();
            if (DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None) SetCurrentFraction();
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
        private void SetCurrentFraction()
        {
            _currentFraction = _fractions.First(fraction => (fraction.Name == DataSaveManager.Instance.MyData.ChoosenFraction));
        }
        public (string, Color) GetCurrentFractionAtributes()
        {
            return (_currentFraction.Name.ToString(), _currentFraction.MainColor);
        }

        #endregion
    }
}

