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
        #region Enums

        public enum UniqueUnitName
        {
            None,
            Human_Warrior,
            Human_Archer,
            Human_Barack,
            Human_Giant
        }

        #endregion

        #region Variables

        [Header("Main Settings")]
        [SerializeField] private UniqueUnitName _uniqueName;
        [SerializeField] private UnitTypeManager.UnitType _type;
        [Space]
        [SerializeField] private UnitLevelsHolder _levelsHolder;
        [SerializeField] private SkinsHolder _skinsHolder;
        [SerializeField] private WeaponInfo _weaponInfo;
        [Space]
        [SerializeField] private Unit _prefab;

        #endregion

        #region Properties

        public UniqueUnitName UniqueName { get => _uniqueName; }
        public UnitTypeManager.UnitType Type { get => _type; }
        public UnitLevelsHolder LevelsHolder { get => _levelsHolder; }
        public SkinsHolder SkinsHolder { get => _skinsHolder; }
        public WeaponInfo WeaponInfo { get => _weaponInfo; }
        public Unit Prefab { get => _prefab; }

        #endregion
    }
}