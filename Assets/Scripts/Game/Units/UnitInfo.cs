using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;
using WOFL.Control;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Unit Info", menuName = "WOFL/Settings/Units/Unit Info", order = 1)]
    public class UnitInfo : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _uniqueName;
        [SerializeField] private UnitTypeManager.UnitType _type;
        [SerializeField] private UnitLevelsHolder _levelsHolder;
        [SerializeField] private SkinsHolder _skinsHolder;
        [SerializeField] private Unit _prefab;

        #endregion

        #region Properties

        public UnitTypeManager.UnitType Type { get => _type; }
        public string UniqueName { get => _uniqueName; }
        public UnitLevelsHolder LevelsHolder { get => _levelsHolder; }
        public SkinsHolder SkinsHolder { get => _skinsHolder; }
        public Unit Prefab { get => _prefab; }

        #endregion
    }
}