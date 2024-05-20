using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Vampire King Unit Level Info", menuName = "WOFL/Settings/Units/Vampire King Unit Level Info", order = 1)]
    public class VampireKingUnitLevelInfo : VampireUnitLevelInfo
    {
        #region Variables

        [Header("Vampire Settings")]
        [SerializeField] private float _delayBetweenSpawn;

        #endregion

        #region Propeties

        public float DelayBetweenSpawn { get => _delayBetweenSpawn; }

        #endregion
    }
}