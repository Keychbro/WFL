using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Weapon Info", menuName = "WOFL/Settings/Units/Weapon Info", order = 1)]
    public class WeaponInfo : ScriptableObject
    {
        #region Variables

        [SerializeField] private WeaponLevelInfo[] _levels;

        #endregion

        #region Properties

        public WeaponLevelInfo[] Levels { get => _levels; }

        #endregion
    }
}