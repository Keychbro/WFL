using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Barracks Info", menuName = "WOFL/Settings/Units/Barracks Info", order = 1)]
    public class BarracksInfo : UnitInfo
    {
        #region Variables

        [SerializeField] private float _delayBetweenSpawnUnit;
        [SerializeField] private UnitInfo.UniqueUnitName _spawnUnitName;

        #endregion

        #region Properties

        public float DelayBetweenSpawnUnit { get => _delayBetweenSpawnUnit; }
        public UnitInfo.UniqueUnitName SpawnUnitName { get => _spawnUnitName; }

        #endregion
    }
}