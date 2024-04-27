using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WOFL.Game
{
    [CreateAssetMenu(fileName = "AI Enemy Battle Settings", menuName = "WOFL/Game/Settings/AI Enemy Battle Settings", order = 1)]
    public class AIEnemySettings : ScriptableObject
    {
        #region Classes

        [Serializable] public struct NewEnemyUnitInfo
        {
            #region NewEnemyUnitInfo Variables

            [SerializeField] private string _uniqueUnitName;
            [SerializeField] private int _unitLevel;
            [SerializeField] private float _spawnDelay;

            #endregion

            #region NewEnemyUnitInfo Properties

            public string UniqueUnitName { get => _uniqueUnitName; }
            public int UnitLevel { get => _unitLevel; }
            public float SpawnDelay { get => _spawnDelay; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private Fraction _enemyFraction;
        [SerializeField] private NewEnemyUnitInfo[] _unitList;
        [SerializeField] private bool _isInfinity;
        [SerializeField] private int _castleHealth;

        #endregion

        #region Properties

        public NewEnemyUnitInfo[] UnitList { get => _unitList; }
        public bool IsInfinity { get => _isInfinity; }
        public int CastleHealth { get => _castleHealth; }

        #endregion
    }
}
