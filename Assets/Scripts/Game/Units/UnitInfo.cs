using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Game;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Unit Info", menuName = "WOFL/Settings/Units/Unit Info", order = 1)]
    public class UnitInfo : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _uniqueName;
        [SerializeField] private UnitLevelsHolder _levels;
        [SerializeField] private SkinsHolder _skins;
        [SerializeField] private Unit _prefab;

        #endregion

        #region Properties

        public string UniqueName { get => _uniqueName; }
        public UnitLevelsHolder Levels { get => _levels; }
        public SkinsHolder Skins { get => _skins; }
        public Unit Prefab { get => _prefab; }

        #endregion
    }
}