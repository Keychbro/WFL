using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    [CreateAssetMenu(fileName = "Fraction", menuName = "WOFL/Settings/Fraction", order = 1)]
    public class Fraction : ScriptableObject
    {
        #region Enums

        public enum FractionName
        {
            Ice,
            Fire,
            Blood,
            Weapon
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private FractionName _name;

        #endregion

        #region Properties

        public FractionName Name { get => _name; }

        #endregion
    }
}