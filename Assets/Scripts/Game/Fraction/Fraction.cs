using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.Game
{
    [CreateAssetMenu(fileName = "Fraction", menuName = "WOFL/Settings/Fraction", order = 1)]
    public class Fraction : ScriptableObject
    {
        #region Enums

        public enum FractionName
        {
            Angle,
            Demon,
            Vampire,
            Human
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private FractionName _name;
        [SerializeField] private CastleSettings _castleSettings;

        #endregion

        #region Properties

        public FractionName Name { get => _name; }
        public CastleSettings CastleSettings { get => _castleSettings; }

        #endregion
    }
}