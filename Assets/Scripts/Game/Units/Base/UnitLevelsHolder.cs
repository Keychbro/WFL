using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Unit Levels Holder", menuName = "WOFL/Settings/Units/Unit Levels Holder", order = 1)]
    public class UnitLevelsHolder : ScriptableObject
    {
        #region Variables

        [SerializeField] private UnitLevelInfo[] _levels;

        #endregion

        #region Properties

        public UnitLevelInfo[] Levels { get => _levels; } 

        #endregion
    }
}

