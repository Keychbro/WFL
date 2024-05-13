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
            Human_Giant,
            Angel_Warrior,
            Angel_Mage,
            Angel_Barrack,
            Angel_King,
            Demon_Warrior,
            Demon_Mage,
            Demon_Barrack,
            Demon_King,
            Vampire_Warrior,
            Vampire_Mage,
            Vampire_Barrack,
            Vampire_King
        }

        #endregion

        #region Variables

        [Header("Main Settings")]
        [SerializeField] protected UniqueUnitName _uniqueName;
        [SerializeField] protected UnitTypeManager.UnitType _type;
        [Space]
        [SerializeField] protected UnitLevelsHolder _levelsHolder;
        [SerializeField] protected SkinsHolder _skinsHolder;
        [SerializeField] protected WeaponInfo _weaponInfo;
        [Space]
        [SerializeField] protected Unit _prefab;

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