using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Game
{
    [CreateAssetMenu(fileName = "Level Settings", menuName = "WOFL/Game/Settings/Level Settings", order = 1)]
    public class LevelSettings : ScriptableObject
    {
        #region Variables

        //TODO: Add other Settings
        [Header("Settings")]
        [SerializeField] private AIEnemySettings _aiEnemySettings;

        #endregion

        #region Properties

        public AIEnemySettings AIEnemySettings { get => _aiEnemySettings; }

        #endregion
    }
}

